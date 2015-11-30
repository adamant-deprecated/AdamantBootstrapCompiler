using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.Runtime;

namespace Adamant.Compiler.Gen.CSharp
{
	public class CSharpWriter
	{
		private readonly TextWriter writer;
		private readonly StringBuilder indent = new StringBuilder();
		private bool firstElement;

		public CSharpWriter(TextWriter writer)
		{
			this.writer = writer;
		}

		public void Write(object value)
		{
			writer.Write(value);
			firstElement = false;
		}

		public void WriteIndented(object value)
		{
			writer.Write(indent);
			writer.Write(value);
			firstElement = false;
		}

		public void WriteIndent()
		{
			writer.Write(indent);
		}

		public void WriteLine(object value)
		{
			writer.WriteLine(value);
			firstElement = false;
		}

		public void WriteIndentedLine(object value)
		{
			writer.Write(indent);
			writer.WriteLine(value);
			firstElement = false;
		}

		public void WriteLine()
		{
			writer.WriteLine();
			firstElement = false;
		}

		public void BlankLine()
		{
			if(firstElement)
				firstElement = false;
			else
				writer.WriteLine();
		}

		public void BeginBlock()
		{
			writer.WriteLine(indent + "{");
			indent.Append('\t');
			firstElement = true;
		}

		public void EndBlock()
		{
			if(indent.Length == 0)
				throw new InvalidOperationException("Can't outdent top level");

			indent.Length = indent.Length - 1;
			writer.WriteLine(indent + "}");
			firstElement = false;
		}

		public void WriteList(IEnumerable<ParserRuleContext> expressionContexts, CSharpGenerator generator)
		{
			var firstParam = true;
			foreach(var argument in expressionContexts)
			{
				if(firstParam)
					firstParam = false;
				else
					Write(", ");

				argument.Accept(generator);
			}
		}
	}
}
