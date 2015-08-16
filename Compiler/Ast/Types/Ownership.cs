namespace Adamant.Compiler.Ast.Types
{
	public enum Ownership
	{
		Implicit = 0,
		Owned = 1,
		MutableBorrow,
		ImmutableBorrow,
	}
}
