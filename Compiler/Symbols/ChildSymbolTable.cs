using System.Collections.Generic;

namespace Adamant.Compiler.Symbols
{
	public class ChildSymbolTable : SymbolTable
	{
		private readonly SymbolTable parent;
		private readonly ISet<string> usingNamespaces = new HashSet<string>();

		public ChildSymbolTable(SymbolTable parent)
		{
			this.parent = parent;
		}

		public void Using(string @namespace)
		{
			usingNamespaces.Add(@namespace);
		}
	}
}
