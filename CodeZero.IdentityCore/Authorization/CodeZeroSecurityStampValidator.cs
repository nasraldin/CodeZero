//  <copyright file="CodeZeroSecurityStampValidator.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.Domain.Uow;
using CodeZero.MultiTenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CodeZero.Authorization
{
    public class CodeZeroSecurityStampValidator<TTenant, TRole, TUser> : SecurityStampValidator<TUser>
        where TTenant : CodeZeroTenant<TUser>
        where TRole : CodeZeroRole<TUser>, new()
        where TUser : CodeZeroUser<TUser>
    {
        public CodeZeroSecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            CodeZeroSignInManager<TTenant, TRole, TUser> signInManager,
            ISystemClock systemClock)
            : base(
                options, 
                signInManager,
                systemClock)
        {
        }

        [UnitOfWork]
        public override Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            return base.ValidateAsync(context);
        }
    }
}
