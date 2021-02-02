﻿using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeZero.Configuration
{
    /// <summary>
    /// Represents the app settings
    /// </summary>
    public partial class AppSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets cache configuration parameters
        /// </summary>
        public CacheConfig CacheConfig { get; set; } = new CacheConfig();

        /// <summary>
        /// Gets or sets hosting configuration parameters
        /// </summary>
        public HostingConfig HostingConfig { get; set; } = new HostingConfig();

        /// <summary>
        /// Gets or sets Redis configuration parameters
        /// </summary>
        public RedisConfig RedisConfig { get; set; } = new RedisConfig();

        /// <summary>
        /// Gets or sets installation configuration parameters
        /// </summary>
        public InstallationConfig InstallationConfig { get; set; } = new InstallationConfig();

        /// <summary>
        /// Gets or sets common configuration parameters
        /// </summary>
        public CommonConfig CommonConfig { get; set; } = new CommonConfig();

        /// <summary>
        /// Gets or sets additional configuration parameters
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JsonDocument> AdditionalData { get; set; }

        #endregion
    }
}