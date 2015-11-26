using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Ast;

namespace Adamant.Compiler.Gen.CSharp
{
	public class GenMetaData
	{
		public static GenMetaData Empty { get; } = new GenMetaData();


		private GenMetaData()
		{
			MainFunctions = new List<QualifiedName>();
		}

		public GenMetaData(string mainFunction)
		{
			MainFunctions = new List<QualifiedName>() { new QualifiedName(mainFunction) };
		}

		public GenMetaData(IEnumerable<QualifiedName> mainFunctions)
		{
			MainFunctions = mainFunctions.ToList();
		}

		public IEnumerable<QualifiedName> MainFunctions { get; }

		public GenMetaData InNamespace(string namespaceName)
		{
			if(!MainFunctions.Any()) return Empty;
			return new GenMetaData(MainFunctions.Select(mf=>mf.Prepend(namespaceName)));
		}
	}
}
