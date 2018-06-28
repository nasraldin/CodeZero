//  <copyright file="ClaimsCodeZeroSession.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using CodeZero.Configuration.Startup;
using CodeZero.Dependency;
using CodeZero.MultiTenancy;
using CodeZero.Runtime.Security;

namespace CodeZero.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="ICodeZeroSession"/> to get session properties from current claims.
    /// </summary>
    public class ClaimsCodeZeroSession : CodeZeroSessionBase, ISingletonDependency
    {
        public override long? UserId
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.UserId;
                }

                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CodeZeroClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public override int? TenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                if (OverridedValue != null)
                {
                    return OverridedValue.TenantId;
                }

                var tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CodeZeroClaimTypes.TenantId);
                if (!string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return Convert.ToInt32(tenantIdClaim.Value);
                }

                if (UserId == null)
                {
                    //Resolve tenant id from request only if user has not logged in!
                    return TenantResolver.ResolveTenantId();
                }
                
                return null;
            }
        }

        public override long? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CodeZeroClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        public override int? ImpersonatorTenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var impersonatorTenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == CodeZeroClaimTypes.ImpersonatorTenantId);
                if (string.IsNullOrEmpty(impersonatorTenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }
        protected ITenantResolver TenantResolver { get; }

        public ClaimsCodeZeroSession(
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(
                  multiTenancy, 
                  sessionOverrideScopeProvider)
        {
            TenantResolver = tenantResolver;
            PrincipalAccessor = principalAccessor;
        }
    }
}