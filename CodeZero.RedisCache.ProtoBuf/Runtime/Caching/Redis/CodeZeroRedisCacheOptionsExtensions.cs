//  <copyright file="CodeZeroRedisCacheOptionsExtensions.cs" project="CodeZero.RedisCache.ProtoBuf" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;

namespace CodeZero.Runtime.Caching.Redis
{
    public static class CodeZeroRedisCacheOptionsExtensions
    {
        public static void UseProtoBuf(this CodeZeroRedisCacheOptions options)
        {
            options.CodeZeroStartupConfiguration
                .ReplaceService<IRedisCacheSerializer, ProtoBufRedisCacheSerializer>(DependencyLifeStyle.Transient);
        }
    }
}
