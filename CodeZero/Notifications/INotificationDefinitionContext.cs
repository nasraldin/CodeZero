//  <copyright file="INotificationDefinitionContext.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Notifications
{
    /// <summary>
    /// Used as a context while defining notifications.
    /// </summary>
    public interface INotificationDefinitionContext
    {
        /// <summary>
        /// Gets the notification definition manager.
        /// </summary>
        INotificationDefinitionManager Manager { get; }
    }
}