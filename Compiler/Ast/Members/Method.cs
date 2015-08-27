using System.Collections.Generic;
using Adamant.Compiler.Ast.Declarations;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Members
{
	public class Method : MethodLike
	{
		public Method(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
		}

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitMethod(this, param);
		}
	}
}
