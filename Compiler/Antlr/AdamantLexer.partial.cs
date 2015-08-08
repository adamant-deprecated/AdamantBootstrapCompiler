using Adamant.Compiler.Preprocessor;
using Antlr4.Runtime;

namespace Adamant.Compiler.Antlr
{
	public partial class AdamantLexer
	{
		private readonly PreprocessorState preprocessorState = new PreprocessorState();
		private PreprocessorVisitor preprocessorVisitor;

		public PreprocessorState PreprocessorState => preprocessorState;

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
			if(preprocessorVisitor == null) preprocessorVisitor = new PreprocessorVisitor(preprocessorState); //annoyingly can't use constructor
			tree.Accept(preprocessorVisitor);
			CurrentMode = preprocessorState.InSkippedSection ? PREPROCESSOR_SKIP : DefaultMode;
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
