using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Adamant.Compiler.Analysis;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Cmd.Config;
using Adamant.Compiler.Cmd.Options;
using Adamant.Compiler.Gen.CSharp;
using Antlr4.Runtime;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using NDesk.Options;
using NDesk.Options.Extensions;
using Newtonsoft.Json;

namespace Adamant.Compiler.Cmd
{
	public class Program
	{
		private const string ProjectFileName = "forge-project.vson";

		private static void Main(string[] args)
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
			if(Directory.Exists(filePath)) // directory path
				filePath = Path.Combine(filePath, ProjectFileName);

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
			var text = token.Text != null
				? "'" + token.Text.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\n", "\\t") + "'"
				: "<no text>";
			var type = AdamantLexer.DefaultVocabulary.GetSymbolicName(token.Type);
			return text + ":" + type + channel + " @" + token.Line + ":" + token.Column;
		}

		private static readonly string[] ChannelNames = { "DEFAULT", "1", "DocComments" };
		private static readonly string[] CoreDependencies = new[] { "Adamant.Compiler.Runtime", "Adamant.Compiler.System" };

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

		private static MainFunctions Compile(string codePath, string outputPath)
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

		private static MainFunctions Compile(string codePath, TextWriter output)
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
			try
			{
				var builtDependencies = new Dictionary<string, Dependency>()
				{
					{"Adamant.Compiler.Runtime", new Dependency(typeof(אRuntime.אArray).Assembly.Location)},
					{"Adamant.Compiler.System", new Dependency(typeof(א.System.Console.Console).Assembly.Location)},
				};
				Forge(projectFilePath, builtDependencies);
			}
			catch(BuildFailedException)
			{
				Console.WriteLine("Build Failed, stopping");
			}
		}

		private static void Forge(string projectFilePath, IDictionary<string, Dependency> builtDependencies)
		{
			var projectDirPath = Path.GetFullPath(Path.GetDirectoryName(projectFilePath));
			var projectConfig = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(projectFilePath));
			projectConfig.Name = projectConfig.Name ?? Path.GetFileName(projectDirPath);

			BuildDependencies(projectDirPath, projectConfig, builtDependencies);
			var targetDirPath = BuildProject(projectDirPath, projectConfig, builtDependencies);
			BuildProjects(projectDirPath, projectConfig, builtDependencies, targetDirPath);
		}

		private static void BuildDependencies(string projectDirPath, ProjectConfig projectConfig,
			IDictionary<string, Dependency> builtDependencies)
		{
			foreach(var dependency in projectConfig.Dependencies)
			{
				var dependencyName = dependency.Key;
				if(builtDependencies.ContainsKey(dependencyName)) continue;
				var path = DependencyPath(dependencyName, dependency.Value, projectDirPath, projectConfig.DependencyPaths);
				Forge(Path.Combine(path, ProjectFileName), builtDependencies);
			}
		}

		private static string DependencyPath(string dependencyName, DependencyConfig config, string projectDirPath,
			IList<string> dependencyPaths)
		{
			if(!string.IsNullOrEmpty(config.Path))
			{
				return Path.Combine(projectDirPath, config.Path);
			}

			foreach(var dependencyPath in dependencyPaths)
			{
				var tryPath = Path.Combine(projectDirPath, dependencyPath, dependencyName);
				if(Directory.Exists(tryPath))
					return tryPath;
			}
			throw new Exception("Could not find dependency");
		}

		private static string BuildProject(string projectDirPath, ProjectConfig projectConfig,
			IDictionary<string, Dependency> builtDependencies)
		{
			Console.WriteLine($"Building {projectConfig.Name} ...");
			var compileDirPath = Path.Combine(projectDirPath, ".bootstrapCompile");
			DeleteDirectoryIfExists(compileDirPath);

			var srcFiles = new DirectoryInfo(Path.Combine(projectDirPath, "src")).GetFiles("*.adam", SearchOption.AllDirectories);

			var mainFunctions = srcFiles.Select(srcFile =>
			{
				var relPath = Path.GetFullPath(srcFile.FullName).Substring(projectDirPath.Length + 1);
				var csPath = Path.ChangeExtension(relPath, "cs");
				return Compile(srcFile.FullName, Path.Combine(compileDirPath, csPath));
			}).Combine();

			if(mainFunctions.Count > 1)
				throw new Exception("Multiple main functions");

			if(mainFunctions.Count == 1)
				GenerateEntryPoint(compileDirPath, mainFunctions.Single());

			var isApp = projectConfig.Template == "app";
			var targetDirPath = Path.Combine(projectDirPath, "targets/debug");
			DeleteDirectoryIfExists(targetDirPath);
			var csSrc = new DirectoryInfo(compileDirPath).GetDirectories("src")
				.Single()
				.GetFiles("*.cs", SearchOption.AllDirectories);
			var assemblyName = projectConfig.Name;
			var assemblyPath = Path.Combine(targetDirPath, assemblyName) + (isApp ? ".exe" : ".dll");
			var dependencies = projectConfig.Dependencies.Keys.Concat(CoreDependencies).ToList();
			var dependencyPaths = dependencies.Select(d => builtDependencies[d].OutputPath);
			CompileCSharp(csSrc, dependencyPaths, assemblyPath, isApp);
			builtDependencies.Add(projectConfig.Name, new Dependency(assemblyPath, dependencies));
			return targetDirPath;
		}

		private static void CompileCSharp(FileInfo[] sources, IEnumerable<string> dependencyPaths, string assemblyPath,
			bool isApp)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(assemblyPath));
			EmitResult result;
			var assemblyFileName = Path.GetFileName(assemblyPath);

			var syntaxTrees =
				sources.Select(src => CSharpSyntaxTree.ParseText(File.ReadAllText(src.FullName), null, src.FullName)).ToArray();
			var references = new List<PortableExecutableReference>()
			{
				MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(DynamicAttribute).Assembly.Location),
			};
			references.AddRange(dependencyPaths.Select(p => MetadataReference.CreateFromFile(p)));
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
				foreach(var group in result.Diagnostics.GroupBy(d => d.Location.SourceTree.FilePath))
				{
					Console.WriteLine($"In {group.Key}");
					foreach(var diagnostic in group)
					{
						Console.WriteLine(diagnostic);
					}
				}
				throw new BuildFailedException();
			}
			var binDir = Path.GetDirectoryName(assemblyPath);
			foreach(var reference in references.Skip(2))
				File.Copy(reference.FilePath, Path.Combine(binDir, Path.GetFileName(reference.FilePath)));
		}

		private static void BuildProjects(string projectDirPath, ProjectConfig projectConfig,
			IDictionary<string, Dependency> builtDependencies, string targetDirPath)
		{
			// Build Projects that weren't already built as dependencies
			foreach(var project in projectConfig.Projects)
			{
				var projectName = project.Key;
				if(builtDependencies.ContainsKey(projectName)) continue;
				Forge(Path.Combine(projectDirPath, project.Value, ProjectFileName), builtDependencies);
				// TODO copy into target
			}
		}

		private static void GenerateEntryPoint(string compileDirPath, MainFunction mainFunction)
		{
			using(var file = File.CreateText(Path.Combine(compileDirPath, "src/אEntryPoint.cs")))
			{
				var returns = mainFunction.ReturnType != "void";
				var returnType = returns ? "int" : "void";
				var returnStatement = returns ? "return " : "";

				file.WriteLine("public class אEntryPoint");
				file.WriteLine("{");
				file.WriteLine($"	public static {returnType} Main(string[] args)");
				file.WriteLine("	{");
				file.WriteLine($"		{returnStatement}{mainFunction.Name}(new א.System.Console.Console(), args);");
				file.WriteLine("	}");
				file.WriteLine("}");
			}
		}

		private static void DeleteDirectoryIfExists(string path)
		{
			for(var i = 0; i < 3; i++)
			{
				try
				{
					if(Directory.Exists(path))
					{
						Directory.Delete(path, true);
						// Having problems with creating dir immediately after deleting
						while(Directory.Exists(path))
							Thread.Sleep(1);
					}
					return; // if no error, don't return
				}
				catch(IOException)
				{
					// Ignore, we want to retry
				}
			}
		}
	}
}
