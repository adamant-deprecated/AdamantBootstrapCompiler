using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Types
{
	public class InferredType : PlainType
	{
		public override TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitInferredType(this, param);
		}
	}
}
