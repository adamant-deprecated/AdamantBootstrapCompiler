using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Expressions
{
	public class LiteralExpression : Expression
	{
		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitLiteral(this, param);
		}
	}
}
