namespace Adamant.Compiler.Ast
{
	public class FunctionDeclaration : Declaration
	{
		public FunctionDeclaration(string name, AccessModifier access) 
			: base(name, access)
		{
		}
	}
}
