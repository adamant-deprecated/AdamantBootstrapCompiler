using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast
{
	public abstract class Type : Node
	{
		public abstract TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param);
	}
}
