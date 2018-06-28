//  <copyright file="CodeZeroQuartzModule.cs" project="CodeZero.Quartz" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Dependency;
using CodeZero.Modules;
using CodeZero.Quartz.Configuration;
using CodeZero.Threading.BackgroundWorkers;
using Quartz;

namespace CodeZero.Quartz
{
    [DependsOn(typeof (CodeZeroKernelModule))]
    public class CodeZeroQuartzModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ICodeZeroQuartzConfiguration, CodeZeroQuartzConfiguration>();

            Configuration.Modules.CodeZeroQuartz().Scheduler.JobFactory = new CodeZeroQuartzJobFactory(IocManager);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IJobListener, CodeZeroQuartzJobListener>();

            Configuration.Modules.CodeZeroQuartz().Scheduler.ListenerManager.AddJobListener(IocManager.Resolve<IJobListener>());

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.Resolve<IBackgroundWorkerManager>().Add(IocManager.Resolve<IQuartzScheduleJobManager>());
            }
        }
    }
}
