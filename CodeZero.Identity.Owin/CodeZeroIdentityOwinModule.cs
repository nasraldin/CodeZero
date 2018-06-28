//  <copyright file="CodeZeroIdentityOwinModule.cs" project="CodeZero.Identity.Owin" solution="CodeZero">
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
using CodeZero.Owin;
using System.Reflection;

namespace CodeZero
{
    [DependsOn(typeof(CodeZeroIdentityCoreModule))]
    public class CodeZeroIdentityOwinModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IUserTokenProviderAccessor, OwinUserTokenProviderAccessor>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
