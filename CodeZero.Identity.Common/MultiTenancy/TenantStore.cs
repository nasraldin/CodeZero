//  <copyright file="TenantStore.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.MultiTenancy
{
    public class TenantStore : ITenantStore
    {
        private readonly ITenantCache _tenantCache;

        public TenantStore(ITenantCache tenantCache)
        {
            _tenantCache = tenantCache;
        }

        public TenantInfo Find(int tenantId)
        {
            var tenant = _tenantCache.GetOrNull(tenantId);
            if (tenant == null)
            {
                return null;
            }

            return new TenantInfo(tenant.Id, tenant.TenancyName);
        }

        public TenantInfo Find(string tenancyName)
        {
            var tenant = _tenantCache.GetOrNull(tenancyName);
            if (tenant == null)
            {
                return null;
            }

            return new TenantInfo(tenant.Id, tenant.TenancyName);
        }
    }
}
