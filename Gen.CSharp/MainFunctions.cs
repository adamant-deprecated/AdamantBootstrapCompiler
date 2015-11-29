using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Adamant.Compiler.Ast;

namespace Adamant.Compiler.Gen.CSharp
{
	public class MainFunctions : IEnumerable<MainFunction>
	{
		public static MainFunctions Empty { get; } = new MainFunctions();

		private readonly IList<MainFunction> funcs;

		private MainFunctions()
		{
			funcs = new List<MainFunction>();
		}

		public MainFunctions(string name, string returnType)
		{
			funcs = new List<MainFunction>() { new MainFunction(new QualifiedName(name), returnType) };
		}

		public MainFunctions(IEnumerable<MainFunction> mainFunctions)
		{
			funcs = mainFunctions.ToList();
		}

		public MainFunctions InNamespace(string namespaceName)
		{
			if(!funcs.Any()) return Empty;
			return new MainFunctions(funcs.Select(mf => mf.InNamespace(namespaceName)));
		}

		public int Count => funcs.Count;

		public IEnumerator<MainFunction> GetEnumerator()
		{
			return funcs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return funcs.GetEnumerator();
		}
	}
}
