namespace Adamant.Compiler.Ast
{
	public class GlobalDeclaration : Declaration
	{
		public GlobalDeclaration(string name, AccessModifier access)
			: base(name, access)
		{
		}
	}
}
