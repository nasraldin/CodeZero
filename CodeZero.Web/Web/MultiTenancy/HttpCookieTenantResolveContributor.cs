//  <copyright file="HttpCookieTenantResolveContributor.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Web;
using CodeZero.Dependency;
using CodeZero.Extensions;
using CodeZero.MultiTenancy;

namespace CodeZero.Web.MultiTenancy
{
    public class HttpCookieTenantResolveContributor : ITenantResolveContributor, ITransientDependency
    {
        public int? ResolveTenantId()
        {
            var cookie = HttpContext.Current?.Request.Cookies[MultiTenancyConsts.TenantIdResolveKey];
            if (cookie == null || cookie.Value.IsNullOrEmpty())
            {
                return null;
            }

            int tenantId;
            return !int.TryParse(cookie.Value, out tenantId) ? (int?) null : tenantId;
        }
    }
}