namespace Adamant.Compiler.Ast
{
	public class GlobalDeclaration : Declaration
	{
		public GlobalDeclaration(AccessModifier access, QualifiedName name)
			: base(access, name)
		{
		}
	}
}
