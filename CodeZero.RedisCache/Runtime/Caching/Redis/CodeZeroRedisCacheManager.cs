//  <copyright file="CodeZeroRedisCacheManager.cs" project="CodeZero.RedisCache" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Runtime.Caching.Configuration;

namespace CodeZero.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to create <see cref="CodeZeroRedisCache"/> instances.
    /// </summary>
    public class CodeZeroRedisCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeZeroRedisCacheManager"/> class.
        /// </summary>
        public CodeZeroRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<CodeZeroRedisCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<CodeZeroRedisCache>(new { name });
        }
    }
}
