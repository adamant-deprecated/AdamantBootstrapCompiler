using System.Collections.Generic;

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

		#region Preprocessor Actions
		private void Preprocess()
		{

		}
		#endregion
	}
}
