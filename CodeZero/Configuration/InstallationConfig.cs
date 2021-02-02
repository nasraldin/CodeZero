namespace CodeZero.Configuration
{
    /// <summary>
    /// Represents installation configuration parameters
    /// </summary>
    public partial class InstallationConfig : IConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether a store owner can install sample data during installation
        /// </summary>
        public bool DisableSampleData { get; set; } = false;
    }
}