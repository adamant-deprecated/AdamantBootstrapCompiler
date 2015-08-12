using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Ast
{
	public class QualifiedName
	{
		public static QualifiedName None = new QualifiedName();

		private readonly string[] names;

		private QualifiedName()
		{
			names = new string[0];
		}

		public QualifiedName(IEnumerable<string> names)
		{
			this.names = names.ToArray();
		}

		public IEnumerable<string> Names => names;
	}
}
