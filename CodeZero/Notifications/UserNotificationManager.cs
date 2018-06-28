//  <copyright file="UserNotificationManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeZero.Dependency;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Implements  <see cref="IUserNotificationManager"/>.
    /// </summary>
    public class UserNotificationManager : IUserNotificationManager, ISingletonDependency
    {
        private readonly INotificationStore _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationManager"/> class.
        /// </summary>
        public UserNotificationManager(INotificationStore store)
        {
            _store = store;
        }

        public async Task<List<UserNotification>> GetUserNotificationsAsync(UserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            var userNotifications = await _store.GetUserNotificationsWithNotificationsAsync(user, state, skipCount, maxResultCount);
            return userNotifications
                .Select(un => un.ToUserNotification())
                .ToList();
        }

        public Task<int> GetUserNotificationCountAsync(UserIdentifier user, UserNotificationState? state = null)
        {
            return _store.GetUserNotificationCountAsync(user, state);
        }

        public async Task<UserNotification> GetUserNotificationAsync(int? tenantId, Guid userNotificationId)
        {
            var userNotification = await _store.GetUserNotificationWithNotificationOrNullAsync(tenantId, userNotificationId);
            if (userNotification == null)
            {
                return null;
            }

            return userNotification.ToUserNotification();
        }

        public Task UpdateUserNotificationStateAsync(int? tenantId, Guid userNotificationId, UserNotificationState state)
        {
            return _store.UpdateUserNotificationStateAsync(tenantId, userNotificationId, state);
        }

        public Task UpdateAllUserNotificationStatesAsync(UserIdentifier user, UserNotificationState state)
        {
            return _store.UpdateAllUserNotificationStatesAsync(user, state);
        }

        public Task DeleteUserNotificationAsync(int? tenantId, Guid userNotificationId)
        {
            return _store.DeleteUserNotificationAsync(tenantId, userNotificationId);
        }

        public Task DeleteAllUserNotificationsAsync(UserIdentifier user)
        {
            return _store.DeleteAllUserNotificationsAsync(user);
        }
    }
}