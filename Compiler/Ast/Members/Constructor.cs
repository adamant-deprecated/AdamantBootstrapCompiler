using System.Collections.Generic;
using Adamant.Compiler.Ast.Declarations;
using Adamant.Compiler.Ast.Visitors;

namespace Adamant.Compiler.Ast.Members
{
	public class Constructor : MethodLike
	{
		public Constructor(AccessModifier access, Name name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
			Name = name;
		}

		public Name Name { get; }

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitConstructor(this, param);
		}
	}
}
