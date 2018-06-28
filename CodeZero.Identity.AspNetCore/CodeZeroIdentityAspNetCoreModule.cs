//  <copyright file="CodeZeroIdentityAspNetCoreModule.cs" project="CodeZero.Identity.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Users;
using CodeZero.Configuration.Startup;
using CodeZero.Identity;
using CodeZero.Modules;
using System.Reflection;

namespace CodeZero.Identity.AspNetCore
{
    [DependsOn(typeof(CodeZeroIdentityCoreModule))]
    public class CodeZeroIdentityAspNetCoreModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroIdentityAspNetCoreConfiguration, CodeZeroIdentityAspNetCoreConfiguration>();
            Configuration.ReplaceService<IUserTokenProviderAccessor, AspNetCoreUserTokenProviderAccessor>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}