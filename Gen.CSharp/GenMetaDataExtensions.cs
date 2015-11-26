using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Gen.CSharp
{
	public static class GenMetaDataExtensions
	{
		public static GenMetaData Combine(this IEnumerable<GenMetaData> metaDatas)
		{
			var withMainFunctions = metaDatas.Where(md => md.MainFunctions.Any()).ToList();
			switch(withMainFunctions.Count)
			{
				case 0:
					return GenMetaData.Empty;
				case 1:
					return withMainFunctions.Single();
				default:
					return new GenMetaData(withMainFunctions.SelectMany(md => md.MainFunctions));
			}
		}
	}
}
