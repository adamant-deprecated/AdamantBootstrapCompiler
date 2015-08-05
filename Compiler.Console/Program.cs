using Adamant.Compiler.Antlr;
using Antlr4.Runtime;

namespace Adamant.Compiler.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var filename = args[0];
			var stream = new AntlrFileStream(filename);
			var lexer = new AdamantLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new AdamantParser(tokens);
			parser.BuildParseTree = true;
			var tree = parser.compilationUnit();
		}
	}
}
