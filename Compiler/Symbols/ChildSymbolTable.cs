using System.Collections.Generic;
using Adamant.Compiler.Ast;

namespace Adamant.Compiler.Symbols
{
	public class ChildSymbolTable : SymbolTable
	{
		private readonly SymbolTable parent;
		private readonly ISet<QualifiedName> usingNamespaces = new HashSet<QualifiedName>();

		public ChildSymbolTable(SymbolTable parent)
		{
			this.parent = parent;
		}

		public void Using(QualifiedName @namespace)
		{
			usingNamespaces.Add(@namespace);
		}
	}
}
