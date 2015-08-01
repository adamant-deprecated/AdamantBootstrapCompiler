namespace Adamant.Compiler
{
	public partial class AdamantLexer
	{
		// While we wait for answer to ANTLR issue #965 we will just declare these
		public const int DocComments = 1;
		public const int Preprocessor = 2;
		private int _channel = 0;
	}
}
