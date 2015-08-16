using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast
{
	public class Assemblage : Node, IDeclarationContainer
	{
		private readonly IList<Declaration> declarations;

		public Assemblage(IEnumerable<IDeclarationContainer> declarationContainers)
		{
			declarations = declarationContainers.SelectMany(c => c.Declarations).ToList();
		}

		public IEnumerable<Declaration> Declarations => declarations;
	}
}
