using System.Collections.Generic;

namespace Adamant.Compiler.Antlr
{
	public partial class AdamantLexer
	{
		// While we wait for answer to ANTLR issue #965 we will just declare these
		public const int DocComments = 1;
		public const int Preprocessor = 2;
		private int _channel = 0;

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

		#region Preprocessor Actions
		private void PreprocessorDefine()
		{
			var symbol = Text.Trim().Split(' ')[1];
			Define(symbol);
		}

		private void PreprocessorUndefine()
		{
			var symbol = Text.Trim().Split(' ')[1];
			Define(symbol);
		}

		private void PreprocessorIf()
		{

		}

		private void PreprocessorElseIf()
		{
		}

		private void PreprocessorElse()
		{
		}

		private void PreprocessorEndif()
		{
		}
		#endregion
	}
}
