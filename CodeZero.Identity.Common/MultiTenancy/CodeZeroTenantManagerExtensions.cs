//  <copyright file="CodeZeroTenantManagerExtensions.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using CodeZero.Authorization.Users;
using CodeZero.Threading;

namespace CodeZero.MultiTenancy
{
    public static class CodeZeroTenantManagerExtensions
    {
        public static void Create<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, TTenant tenant)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.CreateAsync(tenant));
        }

        public static void Update<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, TTenant tenant)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.UpdateAsync(tenant));
        }

        public static TTenant FindById<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int id)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.FindByIdAsync(id));
        }

        public static TTenant GetById<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int id)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.GetByIdAsync(id));
        }

        public static TTenant FindByTenancyName<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, string tenancyName)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.FindByTenancyNameAsync(tenancyName));
        }

        public static void Delete<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, TTenant tenant)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.DeleteAsync(tenant));
        }

        public static string GetFeatureValueOrNull<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int tenantId, string featureName)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.GetFeatureValueOrNullAsync(tenantId, featureName));
        }

        public static IReadOnlyList<NameValue> GetFeatureValues<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int tenantId)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            return AsyncHelper.RunSync(() => tenantManager.GetFeatureValuesAsync(tenantId));
        }

        public static void SetFeatureValues<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int tenantId, params NameValue[] values)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.SetFeatureValuesAsync(tenantId, values));
        }

        public static void SetFeatureValue<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int tenantId, string featureName, string value)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.SetFeatureValueAsync(tenantId, featureName, value));
        }

        public static void SetFeatureValue<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, TTenant tenant, string featureName, string value)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.SetFeatureValueAsync(tenant, featureName, value));
        }

        public static void ResetAllFeatures<TTenant, TUser>(this CodeZeroTenantManager<TTenant, TUser> tenantManager, int tenantId)
            where TTenant : CodeZeroTenant<TUser>
            where TUser : CodeZeroUserBase
        {
            AsyncHelper.RunSync(() => tenantManager.ResetAllFeaturesAsync(tenantId));
        }

    }
}