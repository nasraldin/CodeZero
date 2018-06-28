//  <copyright file="CodeZeroIdentityCoreEntityFrameworkCoreModule.cs" project="CodeZero.IdentityCore.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Castle.MicroKernel.Registration;
using CodeZero.Domain.Uow;
using CodeZero.EntityFrameworkCore;
using CodeZero.Modules;
using CodeZero.MultiTenancy;
using CodeZero.Reflection.Extensions;

namespace CodeZero.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Entity framework integration module for CodeZero Zero.
    /// </summary>
    [DependsOn(typeof(CodeZeroModule), typeof(CodeZeroEntityFrameworkCoreModule))]
    public class CodeZeroIdentityCoreEntityFrameworkCoreModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IConnectionStringResolver, IDbPerTenantConnectionStringResolver>()
                        .ImplementedBy<DbPerTenantConnectionStringResolver>()
                        .LifestyleTransient()
                    );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroIdentityCoreEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
