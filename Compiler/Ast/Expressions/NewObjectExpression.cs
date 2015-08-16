using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast.Expressions
{
	public class NewObjectExpression : Expression
	{
		private readonly IList<Member> members;

		public NewObjectExpression(IEnumerable<Member> members)
		{
			this.members = members.ToList();
		}

		public IEnumerable<Member> Members => members;
	}
}
