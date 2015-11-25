using System.Collections.Generic;

namespace Adamant.Compiler.Cmd
{
	public class Project
	{
		public string Version { get; set; }
		public IList<string> Authors { get; set; }
		public string Template { get; set; }
	}
}
