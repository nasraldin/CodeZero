//  <copyright file="MultiTenancyHelper.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Reflection;
using CodeZero.Domain.Entities;
using CodeZero.Extensions;

namespace CodeZero.MultiTenancy
{
    internal class MultiTenancyHelper
    {
        public static bool IsMultiTenantEntity(object entity)
        {
            return entity is IMayHaveTenant || entity is IMustHaveTenant;
        }

        /// <param name="entity">The entity to check</param>
        /// <param name="expectedTenantId">TenantId or null for host</param>
        public static bool IsTenantEntity(object entity, int? expectedTenantId)
        {
            return (entity is IMayHaveTenant && entity.As<IMayHaveTenant>().TenantId == expectedTenantId) ||
                   (entity is IMustHaveTenant && entity.As<IMustHaveTenant>().TenantId == expectedTenantId);
        }

        public static bool IsHostEntity(object entity)
        {
            MultiTenancySideAttribute attribute = entity.GetType().GetTypeInfo()
                .GetCustomAttributes(typeof(MultiTenancySideAttribute), true)
                .Cast<MultiTenancySideAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                return false;
            }

            return attribute.Side.HasFlag(MultiTenancySides.Host);
        }
    }
}
