namespace Adamant.Compiler.Ast
{
	public abstract class Member : Node
	{
		protected Member(AccessModifier access)
		{
			Access = access;
		}

		public AccessModifier Access { get; }
	}
}
