//  <copyright file="HttpHeaderTenantResolveContributor.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.MultiTenancy;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;

namespace CodeZero.AspNetCore.MultiTenancy
{
    public class HttpHeaderTenantResolveContributor : ITenantResolveContributor, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpHeaderTenantResolveContributor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            Logger = NullLogger.Instance;
        }

        public int? ResolveTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            var tenantIdHeader = httpContext.Request.Headers[MultiTenancyConsts.TenantIdResolveKey];
            if (tenantIdHeader == string.Empty || tenantIdHeader.Count < 1)
            {
                return null;
            }

            if (tenantIdHeader.Count > 1)
            { 
                Logger.Warn(
                    $"HTTP request includes more than one {MultiTenancyConsts.TenantIdResolveKey} header value. First one will be used. All of them: {tenantIdHeader.JoinAsString(", ")}"
                    );
            }

            int tenantId;
            return !int.TryParse(tenantIdHeader.First(), out tenantId) ? (int?) null : tenantId;
        }
    }
}
