namespace Adamant.Compiler.Symbols
{
	public class NullSymbolTable : SymbolTable
	{
		public static NullSymbolTable Instance { get; } = new NullSymbolTable();

		private NullSymbolTable()
		{
		}
	}
}
