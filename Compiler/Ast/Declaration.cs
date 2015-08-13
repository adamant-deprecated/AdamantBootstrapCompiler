namespace Adamant.Compiler.Ast
{
	public abstract class Declaration : Node
	{
		protected Declaration(AccessModifier access, QualifiedName name)
		{
			Name = name;
			Access = access;
		}

		public QualifiedName Name { get; private set; }
		public AccessModifier Access { get; private set; }
	}
}
