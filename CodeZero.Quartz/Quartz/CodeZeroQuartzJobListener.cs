//  <copyright file="CodeZeroQuartzJobListener.cs" project="CodeZero.Quartz" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Quartz;

namespace CodeZero.Quartz
{
    public class CodeZeroQuartzJobListener : IJobListener
    {
        public string Name { get; } = "CodeZeroJobListener";

        public ILogger Logger { get; set; }

        public CodeZeroQuartzJobListener()
        {
            Logger = NullLogger.Instance;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.Debug($"Job {context.JobDetail.JobType.Name} executing...");
            return Task.FromResult(0);
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.Info($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
            return Task.FromResult(0);
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (jobException == null)
            {
                Logger.Debug($"Job {context.JobDetail.JobType.Name} sucessfully executed.");
            }
            else
            {
                Logger.Error($"Job {context.JobDetail.JobType.Name} failed with exception: {jobException}");
            }
            return Task.FromResult(0);
        }
    }
}