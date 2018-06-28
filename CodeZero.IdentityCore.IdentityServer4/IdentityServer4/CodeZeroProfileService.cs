//  <copyright file="CodeZeroProfileService.cs" project="CodeZero.IdentityCore.IdentityServer4" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Authorization.Users;
using CodeZero.Domain.Uow;
using CodeZero.Runtime.Security;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;

namespace CodeZero.IdentityServer4
{
    public class CodeZeroProfileService<TUser> : ProfileService<TUser> 
        where TUser : CodeZeroUser<TUser>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CodeZeroProfileService(
            UserManager<TUser> userManager,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IUnitOfWorkManager unitOfWorkManager
        ) : base(userManager, claimsFactory)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var tenantId = context.Subject.Identity.GetTenantId();
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                await base.GetProfileDataAsync(context);
            }
        }

        [UnitOfWork]
        public override async Task IsActiveAsync(IsActiveContext context)
        {
            var tenantId = context.Subject.Identity.GetTenantId();
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                await base.IsActiveAsync(context);
            }
        }
    }
}
