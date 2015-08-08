/// <summary>
/// Since in C# the System.Void type is not allowed to be used as a generic type argument
/// we have to create this struct to take its place.  Becuase the standard one is in the
/// System namespace which is commonly used, simply putting a Void struct in a
/// namespace would cause ambigous reference errros.  To prevent this, and becuase I view
/// this as a missing language feature (you should actually be allowed to use "void") I have
/// added this struct to the root namespace.
/// </summary>
public struct Void
{
	/// <summary>
	/// A copy of the single void value that can be passed when Void is expected.
	/// </summary>
	public static readonly Void Value = new Void();

	public static bool operator ==(Void left, Void right)
	{
		return true;
	}
	public static bool operator !=(Void left, Void right)
	{
		return false;
	}

	public override bool Equals(object obj)
	{
		return obj is Void;
	}
	public override int GetHashCode()
	{
		return -1876511874;
	}
	public override string ToString()
	{
		return "void";
	}
}
