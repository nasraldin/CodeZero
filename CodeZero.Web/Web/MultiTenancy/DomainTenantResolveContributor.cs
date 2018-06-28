//  <copyright file="DomainTenantResolveContributor.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Web;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.MultiTenancy;
using CodeZero.Text;

namespace CodeZero.Web.MultiTenancy
{
    public class DomainTenantResolveContributor : ITenantResolveContributor, ITransientDependency
    {
        private readonly IWebMultiTenancyConfiguration _multiTenancyConfiguration;
        private readonly ITenantStore _tenantStore;

        public DomainTenantResolveContributor(
            IWebMultiTenancyConfiguration multiTenancyConfiguration,
            ITenantStore tenantStore)
        {
            _multiTenancyConfiguration = multiTenancyConfiguration;
            _tenantStore = tenantStore;
        }

        public int? ResolveTenantId()
        {
            if (_multiTenancyConfiguration.DomainFormat.IsNullOrEmpty())
            {
                return null;
            }

            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return null;
            }

            var hostName = httpContext.Request.Url.Host.RemovePreFix("http://", "https://").RemovePostFix("/");
            var domainFormat = _multiTenancyConfiguration.DomainFormat.RemovePreFix("http://", "https://").Split(':')[0].RemovePostFix("/");
            var result = new FormattedStringValueExtracter().Extract(hostName, domainFormat, true, '/');

            if (!result.IsMatch || !result.Matches.Any())
            {
                return null;
            }

            var tenancyName = result.Matches[0].Value;
            if (tenancyName.IsNullOrEmpty())
            {
                return null;
            }

            if (string.Equals(tenancyName, "www", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var tenantInfo = _tenantStore.Find(tenancyName);
            if (tenantInfo == null)
            {
                return null;
            }

            return tenantInfo.Id;
        }
    }
}