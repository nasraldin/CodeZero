//  <copyright file="CodeZeroIdentityCoreServerEntityFrameworkCoreModule.cs" project="CodeZero.IdentityCore.IdentityServer4.EFCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;
using CodeZero.Identity.EntityFrameworkCore;

namespace CodeZero.IdentityServer4
{
    [DependsOn(typeof(CodeZeroIdentityCoreServerModule), typeof(CodeZeroIdentityCoreEntityFrameworkCoreModule))]
    public class CodeZeroIdentityCoreServerEntityFrameworkCoreModule : CodeZeroModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroIdentityCoreServerEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
