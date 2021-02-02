namespace CodeZero.Shared.Extensions.Numbers
{
    /// <summary>
    /// Float Extensions
    /// </summary>
    public static class FloatExtensions
    {
        #region PercentageOf calculations

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentOf">The percentage of the specified number to get.</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static decimal PercentageOf(this float value, int percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentOf">The percentage of the specified number to get.</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static decimal PercentageOf(this float value, float percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentOf">The percentage of the specified number to get.</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static decimal PercentageOf(this float value, double percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        /// <summary>
        /// Toes the percent.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentOf">The percentage of the specified number to get.</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static decimal PercentageOf(this float value, long percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        #endregion
    }
}
