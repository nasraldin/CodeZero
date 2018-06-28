//  <copyright file="NotificationSeverity.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Notification severity.
    /// </summary>
    public enum NotificationSeverity : byte
    {
        /// <summary>
        /// Info.
        /// </summary>
        Info = 0,
        
        /// <summary>
        /// Success.
        /// </summary>
        Success = 1,
        
        /// <summary>
        /// Warn.
        /// </summary>
        Warn = 2,
        
        /// <summary>
        /// Error.
        /// </summary>
        Error = 3,

        /// <summary>
        /// Fatal.
        /// </summary>
        Fatal = 4
    }
}