//  <copyright file="ICacheConfigurator.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Runtime.Caching.Configuration
{
    /// <summary>
    /// A registered cache configurator.
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// Name of the cache.
        /// It will be null if this configurator configures all caches.
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// Configuration action. Called just after the cache is created.
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}