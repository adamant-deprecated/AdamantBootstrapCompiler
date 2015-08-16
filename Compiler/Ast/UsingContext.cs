using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast
{
	public class UsingContext
	{
		private readonly HashSet<QualifiedName> namespaces;

		public UsingContext()
		{
			namespaces = new HashSet<QualifiedName>();
		}

		public UsingContext(IEnumerable<QualifiedName> namespaces)
		{
			this.namespaces = new HashSet<QualifiedName>(namespaces);
		}

		public UsingContext(UsingContext outerContext, IEnumerable<QualifiedName> namespaces)
		{
			this.namespaces = new HashSet<QualifiedName>(outerContext.namespaces.Union(namespaces));
		}
	}
}
