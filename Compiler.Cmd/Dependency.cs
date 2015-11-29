using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Cmd
{
	public class Dependency
	{
		public Dependency(string outputPath, IEnumerable<string> dependencies)
		{
			OutputPath = outputPath;
			Dependencies = dependencies.ToList();
		}

		public Dependency(string outputPath)
			: this(outputPath, Enumerable.Empty<string>())
		{
		}

		public string OutputPath { get; }
		public IEnumerable<string> Dependencies { get; }
	}
}
