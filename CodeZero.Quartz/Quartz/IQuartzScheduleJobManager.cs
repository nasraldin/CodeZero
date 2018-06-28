//  <copyright file="IQuartzScheduleJobManager.cs" project="CodeZero.Quartz" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Threading.Tasks;
using CodeZero.Threading.BackgroundWorkers;
using Quartz;

namespace CodeZero.Quartz
{
    /// <summary>
    /// Defines interface of Quartz schedule job manager.
    /// </summary>
    public interface IQuartzScheduleJobManager : IBackgroundWorker
    {
        /// <summary>
        /// Schedules a job to be executed.
        /// </summary>
        /// <typeparam name="TJob">Type of the job</typeparam>
        /// <param name="configureJob">Job specific definitions to build.</param>
        /// <param name="configureTrigger">Job specific trigger options which means calendar or time interval.</param>
        /// <returns></returns>
        Task ScheduleAsync<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger) where TJob : IJob;
    }
}