//  <copyright file="TenantNotificationInfoExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Domain.Entities;
using CodeZero.Extensions;
using Newtonsoft.Json;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Extension methods for <see cref="NotificationInfo"/>.
    /// </summary>
    public static class TenantNotificationInfoExtensions
    {
        /// <summary>
        /// Converts <see cref="NotificationInfo"/> to <see cref="TenantNotification"/>.
        /// </summary>
        public static TenantNotification ToTenantNotification(this TenantNotificationInfo tenantNotificationInfo)
        {
            var entityType = tenantNotificationInfo.EntityTypeAssemblyQualifiedName.IsNullOrEmpty()
                ? null
                : Type.GetType(tenantNotificationInfo.EntityTypeAssemblyQualifiedName);

            return new TenantNotification
            {
                Id = tenantNotificationInfo.Id,
                TenantId = tenantNotificationInfo.TenantId,
                NotificationName = tenantNotificationInfo.NotificationName,
                Data = tenantNotificationInfo.Data.IsNullOrEmpty() ? null : JsonConvert.DeserializeObject(tenantNotificationInfo.Data, Type.GetType(tenantNotificationInfo.DataTypeName)) as NotificationData,
                EntityTypeName = tenantNotificationInfo.EntityTypeName,
                EntityType = entityType,
                EntityId = tenantNotificationInfo.EntityId.IsNullOrEmpty() ? null : JsonConvert.DeserializeObject(tenantNotificationInfo.EntityId, EntityHelper.GetPrimaryKeyType(entityType)),
                Severity = tenantNotificationInfo.Severity,
                CreationTime = tenantNotificationInfo.CreationTime
            };
        }
    }
}
