//  <copyright file="CodeZeroRedisCacheModule.cs" project="CodeZero.RedisCache" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;

namespace CodeZero.Runtime.Caching.Redis
{
    /// <summary>
    /// This modules is used to replace CodeZero's cache system with Redis server.
    /// </summary>
    [DependsOn(typeof(CodeZeroKernelModule))]
    public class CodeZeroRedisCacheModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<CodeZeroRedisCacheOptions>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroRedisCacheModule).GetAssembly());
        }
    }
}
