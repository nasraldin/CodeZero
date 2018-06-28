//  <copyright file="CodeZeroUserClaimsPrincipalFactory.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.Dependency;
using CodeZero.Domain.Uow;
using CodeZero.Runtime.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CodeZero.Authorization
{
    public class CodeZeroUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>, ITransientDependency
        where TRole : CodeZeroRole<TUser>, new()
        where TUser : CodeZeroUser<TUser>
    {
        public CodeZeroUserClaimsPrincipalFactory(
            CodeZeroUserManager<TRole, TUser> userManager,
            CodeZeroRoleManager<TRole, TUser> roleManager,
            IOptions<IdentityOptions> optionsAccessor
            ) : base(userManager, roleManager, optionsAccessor)
        {

        }

        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await base.CreateAsync(user);

            if (user.TenantId.HasValue)
            {
                principal.Identities.First().AddClaim(new Claim(CodeZeroClaimTypes.TenantId,user.TenantId.ToString()));
            }

            return principal;
        }
    }
}