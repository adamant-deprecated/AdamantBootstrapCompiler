namespace Adamant.Compiler.Ast.Types
{
	public enum Ownership
	{
		Inferred = 0,
		Owned = 1,
		MutableBorrow,
		ImmutableBorrow,
	}
}
