namespace Adamant.Compiler.Ast
{
	public class ClassDeclaration : Declaration
	{
		public ClassDeclaration(AccessModifier access, QualifiedName name)
			: base(access, name)
		{
		}
	}
}
