//  <copyright file="RunnableBase.cs" project="CodeZero" solution="CodeZero">
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
    /// Base implementation of <see cref="IRunnable"/>.
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        /// <summary>
        /// A boolean value to control the running.
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        private volatile bool _isRunning;

        public virtual void Start()
        {
            _isRunning = true;
        }

        public virtual void Stop()
        {
            _isRunning = false;
        }

        public virtual void WaitToStop()
        {

        }
    }
}