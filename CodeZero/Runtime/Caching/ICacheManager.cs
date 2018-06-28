//  <copyright file="ICacheManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CodeZero.Runtime.Caching
{
    /// <summary>
    /// An upper level container for <see cref="ICache"/> objects. 
    /// A cache manager should work as Singleton and track and manage <see cref="ICache"/> objects.
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// Gets all caches.
        /// </summary>
        /// <returns>List of caches</returns>
        IReadOnlyList<ICache> GetAllCaches();

        /// <summary>
        /// Gets a <see cref="ICache"/> instance.
        /// It may create the cache if it does not already exists.
        /// </summary>
        /// <param name="name">
        /// Unique and case sensitive name of the cache.
        /// </param>
        /// <returns>The cache reference</returns>
        [NotNull] ICache GetCache([NotNull] string name);
    }
}
