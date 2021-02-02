namespace CodeZero.Shared.Extensions.Numbers
{
	/// <summary>
	/// Long Extensions
	/// </summary>
	public static class LongExtensions
	{
		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this long number, int percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this long number, float percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this long number, double percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this long number, decimal percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this long number, long percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this long position, int total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)position / (decimal)total * 100;
			return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this long position, float total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)((decimal)position / (decimal)total * 100);
			return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this long position, double total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)((decimal)position / (decimal)total * 100);
			return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this long position, decimal total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)position / (decimal)total * 100;
			return result;
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this long position, long total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)position / (decimal)total * 100;
			return result;
		}
	}
}
