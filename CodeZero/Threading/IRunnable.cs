//  <copyright file="IRunnable.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Threading
{
    /// <summary>
    /// Interface to start/stop self threaded services.
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        /// Starts the service.
        /// </summary>
        void Start();

        /// <summary>
        /// Sends stop command to the service.
        /// Service may return immediately and stop asynchronously.
        /// A client should then call <see cref="WaitToStop"/> method to ensure it's stopped.
        /// </summary>
        void Stop();

        /// <summary>
        /// Waits the service to stop.
        /// </summary>
        void WaitToStop();
    }
}
