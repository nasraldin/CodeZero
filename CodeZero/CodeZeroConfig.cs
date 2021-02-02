namespace CodeZero
{
    /// <summary>
    /// Represents a CodeZero Config
    /// </summary>
    public class CodeZeroConfig
    {
        /// <summary>
        /// Indicates whether we should use Redis server for caching (instead of default in-memory caching)
        /// </summary>
        public bool RedisCachingEnabled { get; private set; }

        /// <summary>
        /// Redis connection string. Used when Redis caching is enabled
        /// </summary>
        public string RedisCachingConnectionString { get; private set; }

        /// <summary>
        /// A value indicating whether a store owner can install sample data during installation
        /// </summary>
        public bool DisableSampleDataInstallation { get; private set; }

        /// <summary>
        /// Creates a configuration section handler.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, string section)
        {
            var config = new CodeZeroConfig();

            //var redisCachingNode = section.SelectSingleNode("RedisCaching");
            //config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            //config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            return config;
        }
    }
}