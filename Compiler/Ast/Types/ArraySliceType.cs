using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Types
{
	public class ArraySliceType : PlainType
	{
		public ArraySliceType(Type elementType, int dimensions)
		{
			ElementType = elementType;
			Dimensions = dimensions;
		}

		public Type ElementType { get; }
		public int Dimensions { get; }

		public override TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitArraySliceType(this, param);
		}
	}
}
