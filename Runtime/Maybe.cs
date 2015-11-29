namespace אRuntime
{
	public class Maybe<T>
	{
		private T value;

		private Maybe(T value)
		{
			this.value = value;
		}

		public static implicit operator Maybe<T>(T value)
		{
			return new Maybe<T>(value);
		}
	}
}
