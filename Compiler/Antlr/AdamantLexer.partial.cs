using System.Collections.Generic;
using Antlr4.Runtime;

namespace Adamant.Compiler.Antlr
{
	public partial class AdamantLexer
	{
		// While we wait for answer to ANTLR issue #965 we will just declare these
		//private int _channel = 0;

		private IDictionary<string, bool> symbols = new Dictionary<string, bool>();

		public void Define(string symbol)
		{
			symbols[symbol] = true;
		}
		public void Define(string symbol, bool defined)
		{
			symbols[symbol] = defined;
		}
		public void Undefine(string symbol)
		{
			symbols[symbol] = false;
		}

		private void Preprocess()
		{
			if(TokenStartColumn != 0)
				NotifyErrorListeners("Preprocessor directives must be the first non-whitespace character on the line");

			var directive = Text;
			var stream = new AntlrInputStream(directive);
			var lexer = new PreprocessorLineLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new PreprocessorLineParser(tokens);
			parser.BuildParseTree = true;
			var tree = parser.preprocessorLine();
			// TODO visit the tree and apply the action
        }

		public void NotifyErrorListeners(string msg)
		{
			NotifyErrorListeners(msg, null);
		}

		public virtual void NotifyErrorListeners(string msg, RecognitionException e)
		{
			int line = TokenStartLine;
			int charPositionInLine = TokenStartColumn;
			var antlrErrorListener = ErrorListenerDispatch;
			antlrErrorListener.SyntaxError(this, Type, line, charPositionInLine, msg, e);
		}
	}
}
