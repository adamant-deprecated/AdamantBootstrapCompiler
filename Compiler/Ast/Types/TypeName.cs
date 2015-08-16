namespace Adamant.Compiler.Ast.Types
{
	public class TypeName : PlainType
	{
		public TypeName(TypeName outerType, Name name)
		{
			OuterType = outerType;
			Name = name;
		}

		public TypeName OuterType { get; }
		public Name Name { get; }
	}
}
