using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast
{
	public abstract class Expression : Node
	{
		public abstract TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param);
	}
}
