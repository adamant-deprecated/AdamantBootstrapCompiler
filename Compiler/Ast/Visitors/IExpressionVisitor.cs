using Adamant.Compiler.Ast.Expressions;

namespace Adamant.Compiler.Ast.Visitors
{
	public interface IExpressionVisitor<in TParam, out TReturn>
	{
		TReturn VisitLiteral(LiteralExpression expression, TParam param);
		TReturn VisitNewObject(NewObjectExpression expression, TParam param);
	}
}
