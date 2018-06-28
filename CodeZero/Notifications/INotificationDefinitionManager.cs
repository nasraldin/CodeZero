//  <copyright file="INotificationDefinitionManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Used to manage notification definitions.
    /// </summary>
    public interface INotificationDefinitionManager
    {
        /// <summary>
        /// Adds the specified notification definition.
        /// </summary>
        void Add(NotificationDefinition notificationDefinition);

        /// <summary>
        /// Gets a notification definition by name.
        /// Throws exception if there is no notification definition with given name.
        /// </summary>
        NotificationDefinition Get(string name);

        /// <summary>
        /// Gets a notification definition by name.
        /// Returns null if there is no notification definition with given name.
        /// </summary>
        NotificationDefinition GetOrNull(string name);

        /// <summary>
        /// Gets all notification definitions.
        /// </summary>
        IReadOnlyList<NotificationDefinition> GetAll();

        /// <summary>
        /// Checks if given notification (<see cref="name"/>) is available for given user.
        /// </summary>
        Task<bool> IsAvailableAsync(string name, UserIdentifier user);

        /// <summary>
        /// Gets all available notification definitions for given user.
        /// </summary>
        /// <param name="user">User.</param>
        Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(UserIdentifier user);
    }
}