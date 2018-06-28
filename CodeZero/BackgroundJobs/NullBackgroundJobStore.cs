//  <copyright file="NullBackgroundJobStore.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeZero.BackgroundJobs
{
    /// <summary>
    /// Null pattern implementation of <see cref="IBackgroundJobStore"/>.
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// and job execution is not enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// </summary>
    public class NullBackgroundJobStore : IBackgroundJobStore
    {
        public Task<BackgroundJobInfo> GetAsync(long jobId)
        {
            return Task.FromResult(new BackgroundJobInfo());
        }

        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            return Task.FromResult(new List<BackgroundJobInfo>());
        }

        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }
    }
}