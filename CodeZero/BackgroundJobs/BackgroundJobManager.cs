//  <copyright file="BackgroundJobManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using System.Threading.Tasks;
using CodeZero.Dependency;
using CodeZero.Events.Bus;
using CodeZero.Events.Bus.Exceptions;
using CodeZero.Json;
using CodeZero.Threading;
using CodeZero.Threading.BackgroundWorkers;
using CodeZero.Threading.Timers;
using CodeZero.Timing;
using Newtonsoft.Json;

namespace CodeZero.BackgroundJobs
{
    /// <summary>
    /// Default implementation of <see cref="IBackgroundJobManager"/>.
    /// </summary>
    public class BackgroundJobManager : PeriodicBackgroundWorkerBase, IBackgroundJobManager, ISingletonDependency
    {
        public IEventBus EventBus { get; set; }
        
        /// <summary>
        /// Interval between polling jobs from <see cref="IBackgroundJobStore"/>.
        /// Default value: 5000 (5 seconds).
        /// </summary>
        public static int JobPollPeriod { get; set; }

        private readonly IIocResolver _iocResolver;
        private readonly IBackgroundJobStore _store;

        static BackgroundJobManager()
        {
            JobPollPeriod = 5000;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJobManager"/> class.
        /// </summary>
        public BackgroundJobManager(
            IIocResolver iocResolver,
            IBackgroundJobStore store,
            CodeZeroTimer timer)
            : base(timer)
        {
            _store = store;
            _iocResolver = iocResolver;

            EventBus = NullEventBus.Instance;

            Timer.Period = JobPollPeriod;
        }

        public async Task<string> EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = typeof(TJob).AssemblyQualifiedName,
                JobArgs = args.ToJsonString(),
                Priority = priority
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            await _store.InsertAsync(jobInfo);

            return jobInfo.Id.ToString();
        }

        public async Task<bool> DeleteAsync(string jobId)
        {
            if (long.TryParse(jobId, out long finalJobId) == false)
            {
                throw new ArgumentException($"The jobId '{jobId}' should be a number.", nameof(jobId));
            }

            BackgroundJobInfo jobInfo = await _store.GetAsync(finalJobId);
            if (jobInfo == null)
            {
                return false;
            }

            await _store.DeleteAsync(jobInfo);
            return true;
        }

        protected override void DoWork()
        {
            var waitingJobs = AsyncHelper.RunSync(() => _store.GetWaitingJobsAsync(1000));

            foreach (var job in waitingJobs)
            {
                TryProcessJob(job);
            }
        }

        private void TryProcessJob(BackgroundJobInfo jobInfo)
        {
            try
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = Clock.Now;

                var jobType = Type.GetType(jobInfo.JobType);
                using (var job = _iocResolver.ResolveAsDisposable(jobType))
                {
                    try
                    {
                        var jobExecuteMethod = job.Object.GetType().GetTypeInfo().GetMethod("Execute");
                        var argsType = jobExecuteMethod.GetParameters()[0].ParameterType;
                        var argsObj = JsonConvert.DeserializeObject(jobInfo.JobArgs, argsType);

                        jobExecuteMethod.Invoke(job.Object, new[] { argsObj });

                        AsyncHelper.RunSync(() => _store.DeleteAsync(jobInfo));
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.Message, ex);

                        var nextTryTime = jobInfo.CalculateNextTryTime();
                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        TryUpdate(jobInfo);

                        EventBus.Trigger(
                            this,
                            new CodeZeroHandledExceptionData(
                                new BackgroundJobException(
                                    "A background job execution is failed. See inner exception for details. See BackgroundJob property to get information on the background job.", 
                                    ex
                                    )
                                {
                                    BackgroundJob = jobInfo,
                                    JobObject = job.Object
                                }
                            )
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);

                jobInfo.IsAbandoned = true;

                TryUpdate(jobInfo);
            }
        }

        private void TryUpdate(BackgroundJobInfo jobInfo)
        {
            try
            {
                _store.UpdateAsync(jobInfo);
            }
            catch (Exception updateEx)
            {
                Logger.Warn(updateEx.ToString(), updateEx);
            }
        }
    }
}
