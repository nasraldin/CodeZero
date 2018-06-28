//  <copyright file="UserNotificationInfoExtensions.cs" project="CodeZero" solution="CodeZero">
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
    /// Extension methods for <see cref="UserNotificationInfo"/>.
    /// </summary>
    public static class UserNotificationInfoExtensions
    {
        /// <summary>
        /// Converts <see cref="UserNotificationInfo"/> to <see cref="UserNotification"/>.
        /// </summary>
        public static UserNotification ToUserNotification(this UserNotificationInfo userNotificationInfo, TenantNotification tenantNotification)
        {
            return new UserNotification
            {
                Id = userNotificationInfo.Id,
                Notification = tenantNotification,
                UserId = userNotificationInfo.UserId,
                State = userNotificationInfo.State,
                TenantId = userNotificationInfo.TenantId
            };
        }
    }
}