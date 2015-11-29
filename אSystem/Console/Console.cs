using א.System.IO;
using Con = System.Console;

namespace א.System.Console
{
	public class Console
	{
		public void WriteLine(string value)
		{
			Con.WriteLine(value);
		}

		public void WriteLine()
		{
			Con.WriteLine();
		}

		public TextWriter Out => new TextWriter(Con.Out);
	}
}
