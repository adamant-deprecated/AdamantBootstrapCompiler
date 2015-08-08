using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamant.Compiler.Preprocessor
{
	public class PreprocessorState
	{
		private readonly IDictionary<string, bool> symbols = new Dictionary<string, bool>();
		private readonly Stack<Conditional> conditionals = new Stack<Conditional>();

		public void Define(string symbol)
		{
			symbols[symbol] = true;
		}
		public void Define(string symbol, bool defined)
		{
			symbols[symbol] = defined;
		}
		public void Undefine(string symbol)
		{
			symbols[symbol] = false;
		}

		public bool IsDefined(string symbol)
		{
			bool defined;
			return symbols.TryGetValue(symbol, out defined) && defined;
		}

		public void BeginIf(bool value)
		{
			conditionals.Push(Conditional.If(value));
		}

		public void BeginElseIf(bool value)
		{
			// TODO report error if not in conditional
			conditionals.Peek().ElseIf(value);
		}

		public void BeginElse()
		{
			// TODO report error if not in conditional
			conditionals.Peek().Else();
		}

		public void EndIf()
		{
			// TODO report error if not in conditional
			var conditional = conditionals.Pop();
			if(conditional.IsRegion)
				throw new NotImplementedException(); // TODO report error
		}

		public bool InSkippedSection
		{
			get { return conditionals.Any(conditional => conditional.InSkippedSection); }
		}

		public void BeginRegion()
		{
			conditionals.Push(Conditional.BeginRegion());
		}

		public void EndRegion()
		{
			var conditional = conditionals.Pop();
			if(!conditional.IsRegion)
				throw new NotImplementedException(); // TODO report error
		}
	}
}
