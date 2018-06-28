//  <copyright file="CodeZeroHangfireModule.cs" project="CodeZero.HangFire" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Hangfire.Configuration;
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;
using Hangfire;

namespace CodeZero.Hangfire
{
    [DependsOn(typeof(CodeZeroKernelModule))]
    public class CodeZeroHangfireModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroHangfireConfiguration, CodeZeroHangfireConfiguration>();
            
            Configuration.Modules
                .CodeZeroHangfire()
                .GlobalConfiguration
                .UseActivator(new HangfireIocJobActivator(IocManager));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroHangfireModule).GetAssembly());
            GlobalJobFilters.Filters.Add(IocManager.Resolve<CodeZeroHangfireJobExceptionFilter>());
        }
    }
}
