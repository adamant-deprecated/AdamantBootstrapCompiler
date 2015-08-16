namespace Adamant.Compiler.Ast.Declarations
{
	public class GlobalDeclaration : Declaration
	{
		public GlobalDeclaration(AccessModifier access, QualifiedName name, Node initExpression)
			: base(access, name)
		{
		}
	}
}
