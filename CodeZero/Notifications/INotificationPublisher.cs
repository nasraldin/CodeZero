//  <copyright file="INotificationPublisher.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Domain.Entities;
using CodeZero.Runtime.Session;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Used to publish notifications.
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Publishes a new notification.
        /// </summary>
        /// <param name="notificationName">Unique notification name</param>
        /// <param name="data">Notification data (optional)</param>
        /// <param name="entityIdentifier">The entity identifier if this notification is related to an entity</param>
        /// <param name="severity">Notification severity</param>
        /// <param name="userIds">
        /// Target user id(s). 
        /// Used to send notification to specific user(s) (without checking the subscription). 
        /// If this is null/empty, the notification is sent to subscribed users.
        /// </param>
        /// <param name="excludedUserIds">
        /// Excluded user id(s).
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's normally not set if <see cref="userIds"/> is set.
        /// </param>
        /// <param name="tenantIds">
        /// Target tenant id(s).
        /// Used to send notification to subscribed users of specific tenant(s).
        /// This should not be set if <see cref="userIds"/> is set.
        /// <see cref="NotificationPublisher.AllTenants"/> can be passed to indicate all tenants.
        /// But this can only work in a single database approach (all tenants are stored in host database).
        /// If this is null, then it's automatically set to the current tenant on <see cref="ICodeZeroSession.TenantId"/>. 
        /// </param>
        Task PublishAsync(
            string notificationName,
            NotificationData data = null,
            EntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            UserIdentifier[] userIds = null,
            UserIdentifier[] excludedUserIds = null,
            int?[] tenantIds = null);
    }
}