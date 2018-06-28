//  <copyright file="NullRealTimeNotifier.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Null pattern implementation of <see cref="IRealTimeNotifier"/>.
    /// </summary>
    public class NullRealTimeNotifier : IRealTimeNotifier
    {
        /// <summary>
        /// Gets single instance of <see cref="NullRealTimeNotifier"/> class.
        /// </summary>
        public static NullRealTimeNotifier Instance { get { return SingletonInstance; } }
        private static readonly NullRealTimeNotifier SingletonInstance = new NullRealTimeNotifier();

        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            return Task.FromResult(0);
        }
    }
}