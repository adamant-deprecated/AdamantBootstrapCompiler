using System.Diagnostics.Contracts;

namespace Adamant.Compiler.Ast
{
	public struct QualifiedName
	{
		public static QualifiedName None = new QualifiedName();

		private readonly string value;

		public QualifiedName(string name)
		{
			value = string.IsNullOrEmpty(name) ? null : Clean(name);
		}

		[Pure]
		public QualifiedName Append(string name)
		{
			return value == null ? new QualifiedName(name) : new QualifiedName(value + "." + name);
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
			var other = obj as QualifiedName?;
			return other != null && Equals(value, other.Value.value);
		}

		public override string ToString()
		{
			return value;
		}

		public QualifiedName Namespace()
		{
			if(!value.Contains("."))
				return None;

			return new QualifiedName(value.Substring(0, value.LastIndexOf('.')));
		}

		public string Name()
		{
			if(!value.Contains("."))
				return value;

			return value.Substring(value.LastIndexOf('.') + 1);
		}
	}
}
