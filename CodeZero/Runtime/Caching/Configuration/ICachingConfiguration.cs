//  <copyright file="ICachingConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using CodeZero.Configuration.Startup;

namespace CodeZero.Runtime.Caching.Configuration
{
    /// <summary>
    /// Used to configure caching system.
    /// </summary>
    public interface ICachingConfiguration
    {
        /// <summary>
        /// Gets the CodeZero configuration object.
        /// </summary>
        ICodeZeroStartupConfiguration CodeZeroConfiguration { get; }

        /// <summary>
        /// List of all registered configurators.
        /// </summary>
        IReadOnlyList<ICacheConfigurator> Configurators { get; }

        /// <summary>
        /// Used to configure all caches.
        /// </summary>
        /// <param name="initAction">
        /// An action to configure caches
        /// This action is called for each cache just after created.
        /// </param>
        void ConfigureAll(Action<ICache> initAction);

        /// <summary>
        /// Used to configure a specific cache. 
        /// </summary>
        /// <param name="cacheName">Cache name</param>
        /// <param name="initAction">
        /// An action to configure the cache.
        /// This action is called just after the cache is created.
        /// </param>
        void Configure(string cacheName, Action<ICache> initAction);
    }
}
