namespace Adamant.Compiler.Symbols
{
	public abstract class SymbolTable
	{
		public static ChildSymbolTable NewRoot()
		{
			return new ChildSymbolTable(NullSymbolTable.Instance);
		}
	}
}
