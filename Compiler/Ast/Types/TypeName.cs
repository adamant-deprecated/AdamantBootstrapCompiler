using Adamant.Compiler.Ast.Visitors;

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

		public override TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitTypeName(this, param);
		}
	}
}
