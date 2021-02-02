namespace CodeZero.Shared.Extensions.Numbers
{
	/// <summary>
	/// Integer Extensions
	/// </summary>
	public static class IntExtensions
	{
		#region PercentageOf calculations

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this int number, int percent)
		{
			return (decimal)(number * percent / 100);
		}


		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this int position, int total)
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
		public static decimal PercentOf(this int? position, int total)
		{
			if (position == null) return 0;

			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)((decimal)position / (decimal)total * 100);
			return result;
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this int number, float percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this int position, float total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)((decimal)position / (decimal)total * 100);
			return result;
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this int number, double percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this int position, double total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)((decimal)position / (decimal)total * 100);
			return result;
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this int number, decimal percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this int position, decimal total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)position / (decimal)total * 100;
			return result;
		}

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get.</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this int number, long percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns></returns>
		public static decimal PercentOf(this int position, long total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)position / (decimal)total * 100;
			return result;
		}

		#endregion

		public static string ToString(this int? value, string defaultvalue)
		{
			if (value == null) return defaultvalue;
			return value.Value.ToString();
		}
	}
}
