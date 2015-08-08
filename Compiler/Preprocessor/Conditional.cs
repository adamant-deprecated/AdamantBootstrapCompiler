using System;

namespace Adamant.Compiler.Preprocessor
{
	internal class Conditional
	{
		public bool HasTrueSection { get; private set; }
		public bool PastElse { get; private set; }
		public bool InSkippedSection { get; private set; }
		public bool IsRegion { get; private set; }

		private Conditional(bool ifValue)
		{
			HasTrueSection = ifValue;
			InSkippedSection = !ifValue;
		}

		private Conditional()
			:this(true)
		{
			IsRegion = true;
		}

		public static Conditional If(bool value)
		{
			return new Conditional(value);
		}

		public static Conditional BeginRegion()
		{
			return new Conditional();
		}

		public void ElseIf(bool value)
		{
			if(PastElse)
				throw new NotImplementedException(); // TODO report error

			InSkippedSection = !HasTrueSection && value;
			HasTrueSection |= !InSkippedSection;
		}

		public void Else()
		{
			PastElse = true;
			InSkippedSection = HasTrueSection;
			HasTrueSection |= !InSkippedSection;
		}
	}
}
