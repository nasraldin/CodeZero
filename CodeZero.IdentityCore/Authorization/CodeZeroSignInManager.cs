//  <copyright file="CodeZeroSignInManager.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.Configuration;
using CodeZero.Dependency;
using CodeZero.Domain.Uow;
using CodeZero.Extensions;
using CodeZero.MultiTenancy;
using CodeZero.Runtime.Security;
using CodeZero.Identity.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace CodeZero.Authorization
{
    public class CodeZeroSignInManager<TTenant, TRole, TUser> : SignInManager<TUser>, ITransientDependency
        where TTenant : CodeZeroTenant<TUser>
        where TRole : CodeZeroRole<TUser>, new()
        where TUser : CodeZeroUser<TUser>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISettingManager _settingManager;
        private readonly AuthenticationOptions _authenticateOptions;

        public CodeZeroSignInManager(
            CodeZeroUserManager<TRole, TUser> userManager,
            IHttpContextAccessor contextAccessor,
            CodeZeroUserClaimsPrincipalFactory<TUser, TRole> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IAuthenticationSchemeProvider schemes)
            : base(
                userManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                logger,
                schemes)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _settingManager = settingManager;
        }

        public virtual async Task<SignInResult> SignInOrTwoFactorAsync(CodeZeroLoginResult<TTenant, TUser> loginResult, bool isPersistent, bool? rememberBrowser = null, string loginProvider = null, bool bypassTwoFactor = false)
        {
            if (loginResult.Result != CodeZeroLoginResultType.Success)
            {
                throw new ArgumentException("loginResult.Result should be success in order to sign in!");
            }

            using (_unitOfWorkManager.Current.SetTenantId(loginResult.Tenant?.Id))
            {
                await UserManager.As<CodeZeroUserManager<TRole, TUser>>().InitializeOptionsAsync(loginResult.Tenant?.Id);

                if (!bypassTwoFactor && IsTrue(CodeZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled, loginResult.Tenant?.Id))
                {
                    if (await UserManager.GetTwoFactorEnabledAsync(loginResult.User))
                    {
                        if ((await UserManager.GetValidTwoFactorProvidersAsync(loginResult.User)).Count > 0)
                        {
                            if (!await IsTwoFactorClientRememberedAsync(loginResult.User) || rememberBrowser == false)
                            {
                                await Context.SignInAsync(
                                    IdentityConstants.TwoFactorUserIdScheme,
                                    StoreTwoFactorInfo(loginResult.User, loginProvider)
                                );

                                return SignInResult.TwoFactorRequired;
                            }
                        }
                    }
                }

                if (loginProvider != null)
                {
                    await Context.SignOutAsync(IdentityConstants.ExternalScheme);
                }

                await SignInAsync(loginResult.User, isPersistent, loginProvider);
                return SignInResult.Success;
            }
        }

        public virtual async Task SignOutAndSignInAsync(ClaimsIdentity identity, bool isPersistent)
        {
            await SignOutAsync();
            await SignInAsync(identity, isPersistent);
        }

        public virtual async Task SignInAsync(ClaimsIdentity identity, bool isPersistent)
        {
            await Context.SignInAsync(IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(identity),
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = isPersistent }
            );
        }

        [UnitOfWork]
        public override Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            return base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        protected virtual ClaimsPrincipal StoreTwoFactorInfo(TUser user, string loginProvider)
        {
            var identity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));

            if (user.TenantId.HasValue)
            {
                identity.AddClaim(new Claim(CodeZeroClaimTypes.TenantId, user.TenantId.Value.ToString()));
            }

            if (loginProvider != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, loginProvider));
            }

            return new ClaimsPrincipal(identity);
        }

        public async Task<int?> GetVerifiedTenantIdAsync()
        {
            var result = await Context.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);

            if (result?.Principal == null)
            {
                return null;
            }

            return CodeZeroIdentityClaimsIdentityHelper.GetTenantId(result.Principal);
        }

        public override async Task<bool> IsTwoFactorClientRememberedAsync(TUser user)
        {
            var result = await Context.AuthenticateAsync(IdentityConstants.TwoFactorRememberMeScheme);

            return result?.Principal != null &&
                   result.Principal.FindFirstValue(ClaimTypes.Name) == user.Id.ToString() &&
                   CodeZeroIdentityClaimsIdentityHelper.GetTenantId(result.Principal) == user.TenantId;
        }

        public override async Task RememberTwoFactorClientAsync(TUser user)
        {
            var rememberBrowserIdentity = new ClaimsIdentity(IdentityConstants.TwoFactorRememberMeScheme);

            rememberBrowserIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));

            if (user.TenantId.HasValue)
            {
                rememberBrowserIdentity.AddClaim(new Claim(CodeZeroClaimTypes.TenantId, user.TenantId.Value.ToString()));
            }

            await Context.SignInAsync(IdentityConstants.TwoFactorRememberMeScheme,
                new ClaimsPrincipal(rememberBrowserIdentity),
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties { IsPersistent = true });
        }

        private bool IsTrue(string settingName, int? tenantId)
        {
            return tenantId == null
                ? _settingManager.GetSettingValueForApplication<bool>(settingName)
                : _settingManager.GetSettingValueForTenant<bool>(settingName, tenantId.Value);
        }
    }
}
