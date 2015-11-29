using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Gen.CSharp
{
	public static class MainFunctionsExtensions
	{
		public static MainFunctions Combine(this IEnumerable<MainFunctions> mainFunctions)
		{
			var withMainFunctions = mainFunctions.Where(mf => mf.Any()).ToList();
			switch(withMainFunctions.Count)
			{
				case 0:
					return MainFunctions.Empty;
				case 1:
					return withMainFunctions.Single();
				default:
					return new MainFunctions(withMainFunctions.SelectMany(mfs => mfs));
			}
		}
	}
}
