//  <copyright file="NotificationSubscription.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Domain.Entities.Auditing;
using CodeZero.Timing;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Represents a user subscription to a notification.
    /// </summary>
    public class NotificationSubscription : IHasCreationTime
    {
        /// <summary>
        /// Tenant id of the subscribed user.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Notification unique name.
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// Entity type.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Name of the entity type (including namespaces).
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity Id.
        /// </summary>
        public object EntityId { get; set; }

        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscription"/> class.
        /// </summary>
        public NotificationSubscription()
        {
            CreationTime = Clock.Now;
        }
    }
}