using System;

namespace Adamant.Compiler.Ast
{
	public struct Name
	{
		private readonly string value;

		public Name(string name)
		{
			value = string.IsNullOrEmpty(name) ? null : Clean(name);
			if(value != null && value.Contains("."))
				throw new ArgumentException("Name can't contain '.' becuase then it is a QualifiedName");
		}

		private static string Clean(string name)
		{
			return name.Replace("@", "");
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Name?;
			return other != null && Equals(value, other.Value.value);
		}

		public override string ToString()
		{
			return value;
		}
	}
}
