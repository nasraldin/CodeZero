//  <copyright file="DomainTenantResolveContributor.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.MultiTenancy;
using CodeZero.Text;
using CodeZero.Web.MultiTenancy;
using Microsoft.AspNetCore.Http;

namespace CodeZero.AspNetCore.MultiTenancy
{
    public class DomainTenantResolveContributor : ITenantResolveContributor, ITransientDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebMultiTenancyConfiguration _multiTenancyConfiguration;
        private readonly ITenantStore _tenantStore;

        public DomainTenantResolveContributor(
            IHttpContextAccessor httpContextAccessor,
            IWebMultiTenancyConfiguration multiTenancyConfiguration,
            ITenantStore tenantStore)
        {
            _httpContextAccessor = httpContextAccessor;
            _multiTenancyConfiguration = multiTenancyConfiguration;
            _tenantStore = tenantStore;
        }

        public int? ResolveTenantId()
        {
            if (_multiTenancyConfiguration.DomainFormat.IsNullOrEmpty())
            {
                return null;
            }

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }
             
            var hostName = httpContext.Request.Host.Host.RemovePreFix("http://", "https://").RemovePostFix("/");
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