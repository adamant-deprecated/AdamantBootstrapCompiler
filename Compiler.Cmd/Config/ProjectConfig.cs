using System.Collections.Generic;

namespace Adamant.Compiler.Cmd.Config
{
	public class ProjectConfig
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public IList<string> Authors { get; set; }
		public string Template { get; set; }
		public IList<string> DependencyPaths { get; set; } = new List<string>() { ".." };
		public IDictionary<string, DependencyConfig> Dependencies { get; set; } = new Dictionary<string, DependencyConfig>();
		public IDictionary<string, string> Projects { get; set; } = new Dictionary<string, string>();
	}
}
