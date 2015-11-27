using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Adamant.Compiler.Analysis;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Cmd.Options;
using Adamant.Compiler.Gen.CSharp;
using Antlr4.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using NDesk.Options;
using NDesk.Options.Extensions;
using Newtonsoft.Json;
using אRuntime;

namespace Adamant.Compiler.Cmd
{
	public class Program
	{
		private const string ProjectFileName = "forge-project.vson";

		static void Main(string[] args)
		{
			var options = ParseOptions(args);
			if(options == null) return;

			switch(options.Action)
			{
				case CmdAction.Tokenize:
					Tokenize(options.FilePath, options.OutputPath);
					break;

				case CmdAction.PrintTree:
					PrintTree(options.FilePath, options.OutputPath);
					break;

				case CmdAction.Compile:
					Compile(options.FilePath, options.OutputPath);
					break;

				case CmdAction.Forge:
					Forge(options.FilePath);
					break;
				default:
					throw new InvalidEnumArgumentException();
			}
		}

		private static CmdOptions ParseOptions(string[] args)
		{
			var options = new OptionSet();
			var tokenize = options.AddSwitch("tokenize", "Rather than compiling, run the lexer and output the tokens");
			var printTree = options.AddSwitch("tree", "Print the parse tree");
			var outputPath = options.AddVariable<string>("o|output", "Path to write output to");
			var files = options.Parse(args);

			if(files.Count > 1)
			{
				Console.WriteLine("Can only specify one file");
				return null;
			}

			var filePath = files.FirstOrDefault();

			if(tokenize && printTree)
			{
				Console.WriteLine("Can't tokenize and print tree at same time");
				return null;
			}

			if((tokenize || printTree) && IsAdamantCode(filePath))
			{
				Console.WriteLine("When tokenizing or printing parse tree, must specify an adamant source file (*.adam)");
				return null;
			}

			if(tokenize)
			{
				return new CmdOptions()
				{
					FilePath = filePath,
					Action = CmdAction.PrintTree,
					OutputPath = outputPath.Value,
				};
			}

			if(printTree)
			{
				return new CmdOptions()
				{
					FilePath = filePath,
					Action = CmdAction.Tokenize,
					OutputPath = outputPath.Value,
				};
			}

			// Some form of compile

			if(Path.GetExtension(filePath) == ".vson")
			{
				if(Path.GetFileName(filePath) != ProjectFileName)
				{
					Console.WriteLine($"project file must be named {ProjectFileName}");
					return null;
				}

				if(outputPath.Value != null)
				{
					Console.WriteLine($"When compiling {ProjectFileName} file don't specify output path");
					return null;
				}

				return new CmdOptions()
				{
					FilePath = filePath,
					Action = CmdAction.Forge,
				};
			}
			else
			{
				if(!IsAdamantCode(filePath))
				{
					Console.WriteLine("When compiling, must specify an adamant source file (*.adam)");
					return null;
				}

				return new CmdOptions()
				{
					FilePath = filePath,
					Action = CmdAction.Compile,
					OutputPath = outputPath.Value,
				};
			}
		}

		private static bool IsAdamantCode(string filePath)
		{
			return filePath != null && Path.GetExtension(filePath) == ".adam";
		}

		private static void Tokenize(string codePath, string outputPath)
		{
			var output = outputPath != null ? File.CreateText(outputPath) : Console.Out;
			var stream = new AntlrFileStream(codePath);
			var lexer = new AdamantLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			tokens.Fill();
			foreach(var token in tokens.GetTokens())
				output.WriteLine(Format(token));
		}

		private static string Format(IToken token)
		{
			var channel = token.Channel > 0 ? ",channel=" + ChannelNames[token.Channel] : "";
			var text = token.Text != null ? "'" + token.Text.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\n", "\\t") + "'" : "<no text>";
			var type = AdamantLexer.DefaultVocabulary.GetSymbolicName(token.Type);
			return text + ":" + type + channel + " @" + token.Line + ":" + token.Column;
		}

		private static readonly string[] ChannelNames = { "DEFAULT", "1", "DocComments" };

		private static void PrintTree(string codePath, string outputPath)
		{
			var output = outputPath != null ? File.CreateText(outputPath) : Console.Out;
			var stream = new AntlrFileStream(codePath);
			var lexer = new AdamantLexer(stream);
			var tokens = new CommonTokenStream(lexer);

			var parser = new AdamantParser(tokens) { BuildParseTree = true };
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			output.WriteLine(tree.ToStringTree(parser));
		}

		private static GenMetaData Compile(string codePath, string outputPath)
		{
			if(outputPath != null)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
				using(var file = File.CreateText(outputPath))
					return Compile(codePath, file);
			}
			else
				return Compile(codePath, Console.Out);
		}

		private static GenMetaData Compile(string codePath, TextWriter output)
		{
			var stream = new AntlrFileStream(codePath);
			var lexer = new AdamantLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new AdamantParser(tokens) { BuildParseTree = true };
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			//var buildAst = new BuildAstVisitor();
			//var ast = (Assemblage)tree.Accept(buildAst);
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			var cSharpGenerator = new CSharpGenerator(output);
			return tree.Accept(cSharpGenerator);
		}

		private static void Forge(string projectFilePath)
		{
			var project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(projectFilePath));
			var projectDirPath = Path.GetFullPath(Path.GetDirectoryName(projectFilePath));
			var compileDirPath = Path.Combine(projectDirPath, ".bootstrapCompile");
			DeleteDirectoryIfExists(compileDirPath);

			var srcFiles = new DirectoryInfo(Path.Combine(projectDirPath, "src")).GetFiles("*.adam", SearchOption.AllDirectories);

			var metaData = srcFiles.Select(srcFile =>
			{
				var relPath = Path.GetFullPath(srcFile.FullName).Substring(projectDirPath.Length + 1);
				var csPath = Path.ChangeExtension(relPath, "cs");
				return Compile(srcFile.FullName, Path.Combine(compileDirPath, csPath));
			}).Combine();

			var mainFunctions = metaData.MainFunctions.ToList();
			if(mainFunctions.Count > 1)
				throw new Exception("Multiple main functions");

			if(mainFunctions.Count == 1)
				GenerateEntryPoint(compileDirPath, mainFunctions.Single());

			var isApp = project.Template == "app";
			if(isApp)
			{
				// TODO generate entry point
			}

			var binDirPath = Path.Combine(projectDirPath, "bin");
			DeleteDirectoryIfExists(binDirPath);
			var csSrc = new DirectoryInfo(compileDirPath).GetDirectories("src").Single().GetFiles("*.cs", SearchOption.AllDirectories);
			var assemblyName = Path.GetFileName(projectDirPath);
			var assemblyPath = Path.Combine(binDirPath, assemblyName);
			CompileCSharp(csSrc, assemblyPath, isApp);
		}

		private static void GenerateEntryPoint(string compileDirPath, QualifiedName mainFunction)
		{
			using(var file = File.CreateText(Path.Combine(compileDirPath, "src/אEntryPoint.cs")))
			{
				file.WriteLine("public class אEntryPoint");
				file.WriteLine("{");
				file.WriteLine("	public static void Main(string[] args)");
				file.WriteLine("	{");
				file.WriteLine($"		{ mainFunction}(new א.System.Console.Console(), args);");
				file.WriteLine("	}");
				file.WriteLine("}");
			}
		}

		private static void DeleteDirectoryIfExists(string path)
		{
			if(Directory.Exists(path))
				Directory.Delete(path, true);
		}

		private static void CompileCSharp(FileInfo[] sources, string assemblyPath, bool isApp)
		{
			assemblyPath = Path.ChangeExtension(assemblyPath, isApp ? "exe" : "dll");
			Directory.CreateDirectory(Path.GetDirectoryName(assemblyPath));
			EmitResult result;
			var assemblyFileName = Path.GetFileName(assemblyPath);

			var syntaxTrees = sources.Select(src => CSharpSyntaxTree.ParseText(File.ReadAllText(src.FullName))).ToArray();
			var references = new[]
			{
				MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(אArray).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(א.System.Console.Console).Assembly.Location),
			};
			using(var stream = File.Create(assemblyPath))
			{
				var compilation = CSharpCompilation.Create(assemblyFileName,
					syntaxTrees,
					references,
					new CSharpCompilationOptions(isApp ? OutputKind.ConsoleApplication : OutputKind.DynamicallyLinkedLibrary)
					);


				result = compilation.Emit(stream);
			}
			if(!result.Success)
			{
				File.Delete(assemblyPath);
				foreach(var diagnostic in result.Diagnostics)
					Console.WriteLine(diagnostic);
			}
			var binDir = Path.GetDirectoryName(assemblyPath);
			foreach(var reference in references.Skip(1))
			{
				File.Copy(reference.FilePath, Path.Combine(binDir, Path.GetFileName(reference.FilePath)));
			}
		}
	}
}
