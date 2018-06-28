//  <copyright file="CodeZeroIdentityServiceCollectionExtensions.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Application.Editions;
using CodeZero.Application.Features;
using CodeZero.Authorization;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.Identity.Configuration;
using CodeZero.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

// ReSharper disable once CheckNamespace - This is done to add extension methods to Microsoft.Extensions.DependencyInjection namespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class CodeZeroIdentityServiceCollectionExtensions
    {
        public static CodeZeroIdentityBuilder AddCodeZeroIdentity<TTenant, TUser, TRole>(this IServiceCollection services)
            where TTenant : CodeZeroTenant<TUser>
            where TRole : CodeZeroRole<TUser>, new()
            where TUser : CodeZeroUser<TUser>
        {
            return services.AddCodeZeroIdentity<TTenant, TUser, TRole>(setupAction: null);
        }

        public static CodeZeroIdentityBuilder AddCodeZeroIdentity<TTenant, TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction)
            where TTenant : CodeZeroTenant<TUser>
            where TRole : CodeZeroRole<TUser>, new()
            where TUser : CodeZeroUser<TUser>
        {
            services.AddSingleton<ICodeZeroEntityTypes>(new CodeZeroEntityTypes
            {
                Tenant = typeof(TTenant),
                Role = typeof(TRole),
                User = typeof(TUser)
            });

            //CodeZeroTenantManager
            services.TryAddScoped<CodeZeroTenantManager<TTenant, TUser>>();

            //CodeZeroEditionManager
            services.TryAddScoped<CodeZeroEditionManager>();

            //CodeZeroRoleManager
            services.TryAddScoped<CodeZeroRoleManager<TRole, TUser>>();
            services.TryAddScoped(typeof(RoleManager<TRole>), provider => provider.GetService(typeof(CodeZeroRoleManager<TRole, TUser>)));

            //CodeZeroUserManager
            services.TryAddScoped<CodeZeroUserManager<TRole, TUser>>();
            services.TryAddScoped(typeof(UserManager<TUser>), provider => provider.GetService(typeof(CodeZeroUserManager<TRole, TUser>)));

            //SignInManager
            services.TryAddScoped<CodeZeroSignInManager<TTenant, TRole, TUser>>();
            services.TryAddScoped(typeof(SignInManager<TUser>), provider => provider.GetService(typeof(CodeZeroSignInManager<TTenant, TRole, TUser>)));

            //CodeZeroLogInManager
            services.TryAddScoped<CodeZeroLogInManager<TTenant, TRole, TUser>>();

            //CodeZeroUserClaimsPrincipalFactory
            services.TryAddScoped<CodeZeroUserClaimsPrincipalFactory<TUser, TRole>>();
            services.TryAddScoped(typeof(UserClaimsPrincipalFactory<TUser, TRole>), provider => provider.GetService(typeof(CodeZeroUserClaimsPrincipalFactory<TUser, TRole>)));
            services.TryAddScoped(typeof(IUserClaimsPrincipalFactory<TUser>), provider => provider.GetService(typeof(CodeZeroUserClaimsPrincipalFactory<TUser, TRole>)));

            //CodeZeroSecurityStampValidator
            services.TryAddScoped<CodeZeroSecurityStampValidator<TTenant, TRole, TUser>>();
            services.TryAddScoped(typeof(SecurityStampValidator<TUser>), provider => provider.GetService(typeof(CodeZeroSecurityStampValidator<TTenant, TRole, TUser>)));
            services.TryAddScoped(typeof(ISecurityStampValidator), provider => provider.GetService(typeof(CodeZeroSecurityStampValidator<TTenant, TRole, TUser>)));

            //PermissionChecker
            services.TryAddScoped<PermissionChecker<TRole, TUser>>();
            services.TryAddScoped(typeof(IPermissionChecker), provider => provider.GetService(typeof(PermissionChecker<TRole, TUser>)));

            //CodeZeroUserStore
            services.TryAddScoped<CodeZeroUserStore<TRole, TUser>>();
            services.TryAddScoped(typeof(IUserStore<TUser>), provider => provider.GetService(typeof(CodeZeroUserStore<TRole, TUser>)));

            //CodeZeroRoleStore
            services.TryAddScoped<CodeZeroRoleStore<TRole, TUser>>();
            services.TryAddScoped(typeof(IRoleStore<TRole>), provider => provider.GetService(typeof(CodeZeroRoleStore<TRole, TUser>)));

            //CodeZeroFeatureValueStore
            services.TryAddScoped<CodeZeroFeatureValueStore<TTenant, TUser>>();
            services.TryAddScoped(typeof(IFeatureValueStore), provider => provider.GetService(typeof(CodeZeroFeatureValueStore<TTenant, TUser>)));

            return new CodeZeroIdentityBuilder(services.AddIdentity<TUser, TRole>(setupAction), typeof(TTenant));
        }
    }
}
