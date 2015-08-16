namespace Adamant.Compiler.Ast.Declarations
{
	public class FunctionDeclaration : Declaration
	{
		public FunctionDeclaration(AccessModifier access, QualifiedName name)
			: base(access, name)
		{
		}
	}
}
