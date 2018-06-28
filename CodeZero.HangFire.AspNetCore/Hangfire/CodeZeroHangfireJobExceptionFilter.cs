//  <copyright file="CodeZeroHangfireJobExceptionFilter.cs" project="CodeZero.HangFire.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.BackgroundJobs;
using CodeZero.Dependency;
using CodeZero.Events.Bus;
using CodeZero.Events.Bus.Exceptions;
using Hangfire.Common;
using Hangfire.Server;

namespace CodeZero.Hangfire
{
    public class CodeZeroHangfireJobExceptionFilter : JobFilterAttribute, IServerFilter, ITransientDependency
    {
        public IEventBus EventBus { get; set; }

        public CodeZeroHangfireJobExceptionFilter()
        {
            EventBus = NullEventBus.Instance;
        }

        public void OnPerforming(PerformingContext filterContext)
        {
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                EventBus.Trigger(
                    this,
                    new CodeZeroHandledExceptionData(
                        new BackgroundJobException(
                            "A background job execution is failed on Hangfire. See inner exception for details. Use JobObject to get Hangfire background job object.",
                            filterContext.Exception
                        )
                        {
                            JobObject = filterContext.BackgroundJob
                        }
                    )
                );
            }
        }
    }
}
