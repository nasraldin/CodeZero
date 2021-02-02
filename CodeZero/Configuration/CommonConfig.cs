namespace CodeZero.Configuration
{
    /// <summary>
    /// Represents common configuration parameters
    /// </summary>
    public partial class CommonConfig : IConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether to display the full error in production environment. It's ignored (always enabled) in development environment
        /// </summary>
        public bool DisplayFullErrorStack { get; set; } = false;

        /// <summary>
        /// Gets or sets a value that indicates whether to use MiniProfiler services
        /// </summary>
        public bool MiniProfilerEnabled { get; set; } = false;
    }
}