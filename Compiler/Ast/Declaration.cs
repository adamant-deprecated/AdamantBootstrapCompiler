namespace Adamant.Compiler.Ast
{
	public abstract class Declaration : Node
	{
		protected Declaration(string name, AccessModifier access)
		{
			Name = name;
			Access = access;
		}

		public string Name { get; private set; }
		public AccessModifier Access { get; private set; }
	}
}
