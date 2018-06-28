//  <copyright file="RunnableExtensions.cs" project="CodeZero" solution="CodeZero">
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
    /// Some extension methods for <see cref="IRunnable"/>.
    /// </summary>
    public static class RunnableExtensions
    {
        /// <summary>
        /// Calls <see cref="IRunnable.Stop"/> and then <see cref="IRunnable.WaitToStop"/>.
        /// </summary>
        public static void StopAndWaitToStop(this IRunnable runnable)
        {
            runnable.Stop();
            runnable.WaitToStop();
        }
    }
}