//  <copyright file="IBackgroundJobStore.cs" project="CodeZero" solution="CodeZero">
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
    /// Defines interface to store/get background jobs.
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// Gets a BackgroundJobInfo based on the given jobId.
        /// </summary>
        /// <param name="jobId">The Job Unique Identifier.</param>
        /// <returns>The BackgroundJobInfo object.</returns>
        Task<BackgroundJobInfo> GetAsync(long jobId);

        /// <summary>
        /// Inserts a background job.
        /// </summary>
        /// <param name="jobInfo">Job information.</param>
        Task InsertAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Gets waiting jobs. It should get jobs based on these:
        /// Conditions: !IsAbandoned And NextTryTime &lt;= Clock.Now.
        /// Order by: Priority DESC, TryCount ASC, NextTryTime ASC.
        /// Maximum result: <paramref name="maxResultCount"/>.
        /// </summary>
        /// <param name="maxResultCount">Maximum result count.</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount);

        /// <summary>
        /// Deletes a job.
        /// </summary>
        /// <param name="jobInfo">Job information.</param>
        Task DeleteAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Updates a job.
        /// </summary>
        /// <param name="jobInfo">Job information.</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}