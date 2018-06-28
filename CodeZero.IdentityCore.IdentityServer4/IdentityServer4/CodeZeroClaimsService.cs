//  <copyright file="CodeZeroClaimsService.cs" project="CodeZero.IdentityCore.IdentityServer4" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CodeZero.Runtime.Security;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace CodeZero.IdentityServer4
{
    public class CodeZeroClaimsService : DefaultClaimsService
    {
        public CodeZeroClaimsService(IProfileService profile, ILogger<DefaultClaimsService> logger)
            : base(profile, logger)
        {
        }

        protected override IEnumerable<Claim> GetOptionalClaims(ClaimsPrincipal subject)
        {
            var tenantClaim = subject.FindFirst(CodeZeroClaimTypes.TenantId);
            if (tenantClaim == null)
            {
                return base.GetOptionalClaims(subject);
            }
            else
            {
                return base.GetOptionalClaims(subject).Union(new[] { tenantClaim });
            }
        }
    }
}
