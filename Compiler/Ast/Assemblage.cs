using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast
{
	public class Assemblage : Node
	{
		private readonly IList<Declaration> declarations;

		public Assemblage(IEnumerable<Declaration> declarations)
		{
			this.declarations = declarations.ToList();
		}

		public IEnumerable<Declaration> Declarations => declarations;
	}
}
