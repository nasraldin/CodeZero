//  <copyright file="UserNotificationInfoWithNotificationInfo.cs" project="CodeZero" solution="CodeZero">
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
    /// A class contains a <see cref="UserNotificationInfo"/> and related <see cref="NotificationInfo"/>.
    /// </summary>
    public class UserNotificationInfoWithNotificationInfo
    {
        /// <summary>
        /// User notification.
        /// </summary>
        public UserNotificationInfo UserNotification { get; set; }

        /// <summary>
        /// Notification.
        /// </summary>
        public TenantNotificationInfo Notification { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationInfoWithNotificationInfo"/> class.
        /// </summary>
        public UserNotificationInfoWithNotificationInfo(UserNotificationInfo userNotification, TenantNotificationInfo notification)
        {
            UserNotification = userNotification;
            Notification = notification;
        }
    }
}