namespace CodeZero.Shared.Extensions.Numbers
{
	/// <summary>
	/// Decimal Extensions
	/// </summary>
	public static class DecimalExtensions
	{
		#region PercentageOf calculations

		/// <summary>
		/// The numbers percentage
		/// </summary>
		/// <param name="number">The number to get the percentage of.</param>
		/// <param name="percent">The percentage of the specified number to get</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this decimal number, int percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this decimal position, int total)
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
		/// <param name="percent">The percentage of the specified number to get</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		public static decimal PercentageOf(this decimal number, decimal percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this decimal position, decimal total)
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
		/// <param name="percent">The percentage of the specified number to get</param>
		/// <returns>The actual specified percentage of the specified number.</returns>
		/// <returns>The result</returns>
		public static decimal PercentageOf(this decimal number, long percent)
		{
			return (decimal)(number * percent / 100);
		}

		/// <summary>
		/// Percentage of the number.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="total"></param>
		/// <returns>The result</returns>
		public static decimal PercentOf(this decimal position, long total)
		{
			decimal result = 0;
			if (position > 0 && total > 0)
				result = (decimal)position / (decimal)total * 100;
			return result;
		}

		#endregion
	}
}
