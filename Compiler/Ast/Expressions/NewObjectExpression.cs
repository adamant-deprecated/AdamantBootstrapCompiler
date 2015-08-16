using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast.Expressions
{
	public class NewObjectExpression : Expression
	{
		private readonly IList<TypeNode> interfaces;
		public TypeNode BaseClass { get;  }
		private readonly IList<Member> members;

		public NewObjectExpression(TypeNode baseClass, IEnumerable<TypeNode> interfaces, IEnumerable<Member> members)
		{
			this.interfaces = interfaces.ToList();
			BaseClass = baseClass;
			this.members = members.ToList();
		}

		public IEnumerable<Member> Members => members;
	}
}
