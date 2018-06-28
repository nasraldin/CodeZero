//  <copyright file="CodeZeroRedisCacheDatabaseProvider.cs" project="CodeZero.RedisCache" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Dependency;
using StackExchange.Redis;

namespace CodeZero.Runtime.Caching.Redis
{
    /// <summary>
    /// Implements <see cref="ICodeZeroRedisCacheDatabaseProvider"/>.
    /// </summary>
    public class CodeZeroRedisCacheDatabaseProvider : ICodeZeroRedisCacheDatabaseProvider, ISingletonDependency
    {
        private readonly CodeZeroRedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeZeroRedisCacheDatabaseProvider"/> class.
        /// </summary>
        public CodeZeroRedisCacheDatabaseProvider(CodeZeroRedisCacheOptions options)
        {
            _options = options;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_options.ConnectionString);
        }
    }
}
