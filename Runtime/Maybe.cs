namespace אRuntime
{
	public struct Maybe<T>
	{
		private readonly T value;

		private Maybe(T value)
		{
			this.value = value;
		}

		public static implicit operator Maybe<T>(T value)
		{
			return new Maybe<T>(value);
		}

		public static bool operator ==(Maybe<T> left, Maybe<T> right)
		{
			return Equals(left.value, right.value);
		}

		public static bool operator !=(Maybe<T> left, Maybe<T> right)
		{
			return !(left == right);
		}
	}
}
