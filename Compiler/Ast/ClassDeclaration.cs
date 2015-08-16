namespace Adamant.Compiler.Ast
{
	public class ClassDeclaration : Declaration
	{
		public ClassDeclaration(AccessModifier access, bool isPartial, Safety safety, bool isSealed, bool isAbstract, QualifiedName name)
			: base(access, name)
		{
			IsPartial = isPartial;
			Safety = safety;
			IsSealed = isSealed;
			IsAbstract = isAbstract;
		}

		public bool IsPartial { get; }
		public Safety Safety { get; }
		public bool IsSealed { get; }
		public bool IsAbstract { get; }
	}
}
