//  <copyright file="INotificationSubscriptionManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeZero.Domain.Entities;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Used to manage subscriptions for notifications.
    /// </summary>
    public interface INotificationSubscriptionManager
    {
        /// <summary>
        /// Subscribes to a notification for given user and notification informations.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task SubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Subscribes to all available notifications for given user.
        /// It does not subscribe entity related notifications.
        /// </summary>
        /// <param name="user">User.</param>
        Task SubscribeToAllAvailableNotificationsAsync(UserIdentifier user);

        /// <summary>
        /// Unsubscribes from a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task UnsubscribeAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets all subscribtions for given notification (including all tenants).
        /// This only works for single database approach in a multitenant application!
        /// </summary>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets all subscribtions for given notification.
        /// </summary>
        /// <param name="tenantId">Tenant id. Null for the host.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(int? tenantId, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets subscribed notifications for a user.
        /// </summary>
        /// <param name="user">User.</param>
        Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(UserIdentifier user);

        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<bool> IsSubscribedAsync(UserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null);
    }
}
