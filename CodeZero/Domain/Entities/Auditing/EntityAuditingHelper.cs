//  <copyright file="EntityAuditingHelper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Timing;
using System;
using CodeZero.Configuration.Startup;
using CodeZero.MultiTenancy;
using CodeZero.Extensions;

namespace CodeZero.Domain.Entities.Auditing
{
    public static class EntityAuditingHelper
    {
        public static void SetCreationAuditProperties(
            IMultiTenancyConfig multiTenancyConfig, 
            object entityAsObj, 
            int? tenantId,
            long? userId)
        {
            var entityWithCreationTime = entityAsObj as IHasCreationTime;
            if (entityWithCreationTime == null)
            {
                //Object does not implement IHasCreationTime
                return;
            }

            if (entityWithCreationTime.CreationTime == default(DateTime))
            {
                entityWithCreationTime.CreationTime = Clock.Now;
            }

            if (!(entityAsObj is ICreationAudited))
            {
                //Object does not implement ICreationAudited
                return;
            }

            if (!userId.HasValue)
            {
                //Unknown user
                return;
            }

            var entity = entityAsObj as ICreationAudited;
            if (entity.CreatorUserId != null)
            {
                //CreatorUserId is already set
                return;
            }

            if (multiTenancyConfig?.IsEnabled == true)
            {
                if (MultiTenancyHelper.IsMultiTenantEntity(entity) &&
                    !MultiTenancyHelper.IsTenantEntity(entity, tenantId))
                {
                    //A tenant entitiy is created by host or a different tenant
                    return;
                }

                if (tenantId.HasValue && MultiTenancyHelper.IsHostEntity(entity))
                {
                    //Tenant user created a host entity
                    return;
                }
            }

            //Finally, set CreatorUserId!
            entity.CreatorUserId = userId;
        }

        public static void SetModificationAuditProperties(
            IMultiTenancyConfig multiTenancyConfig,
            object entityAsObj,
            int? tenantId,
            long? userId)
        {
            if (entityAsObj is IHasModificationTime)
            {
                entityAsObj.As<IHasModificationTime>().LastModificationTime = Clock.Now;
            }

            if (!(entityAsObj is IModificationAudited))
            {
                //Entity does not implement IModificationAudited
                return;
            }

            var entity = entityAsObj.As<IModificationAudited>();

            if (userId == null)
            {
                //Unknown user
                entity.LastModifierUserId = null;
                return;
            }

            if (multiTenancyConfig?.IsEnabled == true)
            {
                if (MultiTenancyHelper.IsMultiTenantEntity(entity) &&
                    !MultiTenancyHelper.IsTenantEntity(entity, tenantId))
                {
                    //A tenant entitiy is modified by host or a different tenant
                    entity.LastModifierUserId = null;
                    return;
                }

                if (tenantId.HasValue && MultiTenancyHelper.IsHostEntity(entity))
                {
                    //Tenant user modified a host entity
                    entity.LastModifierUserId = null;
                    return;
                }
            }

            //Finally, set LastModifierUserId!
            entity.LastModifierUserId = userId;
        }
    }
}
