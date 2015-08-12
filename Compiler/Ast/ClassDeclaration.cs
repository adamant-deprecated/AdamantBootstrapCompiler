namespace Adamant.Compiler.Ast
{
	public class ClassDeclaration : Declaration
	{
		public ClassDeclaration(string name, AccessModifier access) 
			: base(name, access)
		{
		}
	}
}
