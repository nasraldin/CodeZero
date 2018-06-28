//  <copyright file="IBackgroundWorkerManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Threading.BackgroundWorkers
{
    /// <summary>
    /// Used to manage background workers.
    /// </summary>
    public interface IBackgroundWorkerManager : IRunnable
    {
        /// <summary>
        /// Adds a new worker. Starts the worker immediately if <see cref="IBackgroundWorkerManager"/> has started.
        /// </summary>
        /// <param name="worker">
        /// The worker. It should be resolved from IOC.
        /// </param>
        void Add(IBackgroundWorker worker);
    }
}