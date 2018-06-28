//  <copyright file="EntityExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Domain.Entities.Auditing;
using CodeZero.Extensions;

namespace CodeZero.Domain.Entities
{
    /// <summary>
    /// Some useful extension methods for Entities.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Check if this Entity is null of marked as deleted.
        /// </summary>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }

        /// <summary>
        /// Undeletes this entity by setting <see cref="ISoftDelete.IsDeleted"/> to false and
        /// <see cref="IDeletionAudited"/> properties to null.
        /// </summary>
        public static void UnDelete(this ISoftDelete entity)
        {
            entity.IsDeleted = false;
            if (entity is IDeletionAudited)
            {
                var deletionAuditedEntity = entity.As<IDeletionAudited>();
                deletionAuditedEntity.DeletionTime = null;
                deletionAuditedEntity.DeleterUserId = null;
            }
        }
    }
}