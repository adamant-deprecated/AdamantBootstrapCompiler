namespace Adamant.Compiler.Ast
{
	public class FunctionDeclaration : Declaration
	{
		public FunctionDeclaration(AccessModifier access, QualifiedName name)
			: base(access, name)
		{
		}
	}
}
