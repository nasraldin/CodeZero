//  <copyright file="BackgroundJobStore.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeZero.Dependency;
using CodeZero.Domain.Repositories;
using CodeZero.Domain.Uow;
using CodeZero.Timing;

namespace CodeZero.BackgroundJobs
{
    /// <summary>
    /// Implements <see cref="IBackgroundJobStore"/> using repositories.
    /// </summary>
    public class BackgroundJobStore : IBackgroundJobStore, ITransientDependency
    {
        private readonly IRepository<BackgroundJobInfo, long> _backgroundJobRepository;

        public BackgroundJobStore(IRepository<BackgroundJobInfo, long> backgroundJobRepository)
        {
            _backgroundJobRepository = backgroundJobRepository;
        }

        public Task<BackgroundJobInfo> GetAsync(long jobId)
        {
            return _backgroundJobRepository.GetAsync(jobId);
        }

        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            return _backgroundJobRepository.InsertAsync(jobInfo);
        }

        [UnitOfWork]
        public virtual Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            var waitingJobs = _backgroundJobRepository.GetAll()
                .Where(t => !t.IsAbandoned && t.NextTryTime <= Clock.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();

            return Task.FromResult(waitingJobs);
        }

        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return _backgroundJobRepository.DeleteAsync(jobInfo);
        }

        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return _backgroundJobRepository.UpdateAsync(jobInfo);
        }
    }
}
