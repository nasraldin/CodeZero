//  <copyright file="NullFeatureValueStore.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Null pattern (default) implementation of <see cref="IFeatureValueStore"/>.
    /// It gets null for all feature values.
    /// <see cref="Instance"/> can be used via property injection of <see cref="IFeatureValueStore"/>.
    /// </summary>
    public class NullFeatureValueStore : IFeatureValueStore
    {
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static NullFeatureValueStore Instance { get { return SingletonInstance; } }
        private static readonly NullFeatureValueStore SingletonInstance = new NullFeatureValueStore();

        /// <inheritdoc/>
        public Task<string> GetValueOrNullAsync(int tenantId, Feature feature)
        {
            return Task.FromResult((string) null);
        }
    }
}