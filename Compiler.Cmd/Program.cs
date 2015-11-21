using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Adamant.Compiler.Analysis;
using Adamant.Compiler.Antlr;
using Adamant.Compiler.Ast;
using Adamant.Compiler.Cmd.Options;
using Adamant.Compiler.Gen.CSharp;
using Adamant.Compiler.Translation;
using Antlr4.Runtime;
using NDesk.Options;
using NDesk.Options.Extensions;

namespace Adamant.Compiler.Cmd
{
	public class Program
	{
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
				if(Path.GetFileName(filePath) != "project.vson")
				{
					Console.WriteLine("project file must be named project.vson");
					return null;
				}

				if(outputPath.Value != null)
				{
					Console.WriteLine("When compiling project.vson file don't specify output path");
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

		private static void Compile(string codePath, string outputPath)
		{
			var output = outputPath != null ? File.CreateText(outputPath) : Console.Out;
			var stream = new AntlrFileStream(codePath);
			var lexer = new AdamantLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new AdamantParser(tokens) { BuildParseTree = true };
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			var buildAst = new BuildAstVisitor();
			var ast = (Assemblage)tree.Accept(buildAst);
			var borrowChecker = new BorrowChecker();
			borrowChecker.Check(ast);
			var cSharpGenerator = new CSharpGenerator(output);
			cSharpGenerator.GenerateFor(ast);
		}

		private static void Forge(string projectFilePath)
		{
			throw new NotImplementedException();
		}
	}
}
