//  <copyright file="CodeZeroIdentityBuilderExtensions.cs" project="CodeZero.IdentityCore" solution="CodeZero">
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
using Microsoft.AspNetCore.Identity;
using CodeZero.Authorization.Users;
using CodeZero.Authorization.Roles;
using CodeZero.MultiTenancy;

// ReSharper disable once CheckNamespace - This is done to add extension methods to Microsoft.Extensions.DependencyInjection namespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class CodeZeroIdentityIdentityBuilderExtensions
    {
        public static CodeZeroIdentityBuilder AddCodeZeroTenantManager<TTenantManager>(this CodeZeroIdentityBuilder builder)
            where TTenantManager : class
        {
            var type = typeof(TTenantManager);
            var CodeZeroManagerType = typeof(CodeZeroTenantManager<,>).MakeGenericType(builder.TenantType, builder.UserType);
            builder.Services.AddScoped(type, provider => provider.GetRequiredService(CodeZeroManagerType));
            builder.Services.AddScoped(CodeZeroManagerType, type);
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroEditionManager<TEditionManager>(this CodeZeroIdentityBuilder builder)
            where TEditionManager : class
        {
            var type = typeof(TEditionManager);
            var CodeZeroManagerType = typeof(CodeZeroEditionManager);
            builder.Services.AddScoped(type, provider => provider.GetRequiredService(CodeZeroManagerType));
            builder.Services.AddScoped(CodeZeroManagerType, type);
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroRoleManager<TRoleManager>(this CodeZeroIdentityBuilder builder)
            where TRoleManager : class
        {
            var CodeZeroManagerType = typeof(CodeZeroRoleManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(RoleManager<>).MakeGenericType(builder.RoleType);
            builder.Services.AddScoped(CodeZeroManagerType, services => services.GetRequiredService(managerType));
            builder.AddRoleManager<TRoleManager>();
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroUserManager<TUserManager>(this CodeZeroIdentityBuilder builder)
            where TUserManager : class
        {
            var CodeZeroManagerType = typeof(CodeZeroUserManager<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var managerType = typeof(UserManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(CodeZeroManagerType, services => services.GetRequiredService(managerType));
            builder.AddUserManager<TUserManager>();
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroSignInManager<TSignInManager>(this CodeZeroIdentityBuilder builder)
            where TSignInManager : class
        {
            var CodeZeroManagerType = typeof(CodeZeroSignInManager<,,>).MakeGenericType(builder.TenantType, builder.RoleType, builder.UserType);
            var managerType = typeof(SignInManager<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(CodeZeroManagerType, services => services.GetRequiredService(managerType));
            builder.AddSignInManager<TSignInManager>();
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroLogInManager<TLogInManager>(this CodeZeroIdentityBuilder builder)
            where TLogInManager : class
        {
            var type = typeof(TLogInManager);
            var CodeZeroManagerType = typeof(CodeZeroLogInManager<,,>).MakeGenericType(builder.TenantType, builder.RoleType, builder.UserType);
            builder.Services.AddScoped(type, provider => provider.GetService(CodeZeroManagerType));
            builder.Services.AddScoped(CodeZeroManagerType, type);
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroUserClaimsPrincipalFactory<TUserClaimsPrincipalFactory>(this CodeZeroIdentityBuilder builder)
            where TUserClaimsPrincipalFactory : class
        {
            var type = typeof(TUserClaimsPrincipalFactory);
            builder.Services.AddScoped(typeof(UserClaimsPrincipalFactory<,>).MakeGenericType(builder.UserType, builder.RoleType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(CodeZeroUserClaimsPrincipalFactory<,>).MakeGenericType(builder.UserType, builder.RoleType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(IUserClaimsPrincipalFactory<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroSecurityStampValidator<TSecurityStampValidator>(this CodeZeroIdentityBuilder builder)
            where TSecurityStampValidator : class, ISecurityStampValidator
        {
            var type = typeof(TSecurityStampValidator);
            builder.Services.AddScoped(typeof(SecurityStampValidator<>).MakeGenericType(builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(CodeZeroSecurityStampValidator<,,>).MakeGenericType(builder.TenantType, builder.RoleType, builder.UserType), services => services.GetRequiredService(type));
            builder.Services.AddScoped(typeof(ISecurityStampValidator), services => services.GetRequiredService(type));
            builder.Services.AddScoped(type);
            return builder;
        }

        public static CodeZeroIdentityBuilder AddPermissionChecker<TPermissionChecker>(this CodeZeroIdentityBuilder builder)
            where TPermissionChecker : class
        {
            var type = typeof(TPermissionChecker);
            var checkerType = typeof(PermissionChecker<,>).MakeGenericType(builder.RoleType, builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(checkerType, provider => provider.GetService(type));
            builder.Services.AddScoped(typeof(IPermissionChecker), provider => provider.GetService(type));
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroUserStore<TUserStore>(this CodeZeroIdentityBuilder builder)
            where TUserStore : class
        {
            var type = typeof(TUserStore);
            var CodeZeroStoreType = typeof(CodeZeroUserStore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var storeType = typeof(IUserStore<>).MakeGenericType(builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(CodeZeroStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(storeType, services => services.GetRequiredService(type));
            return builder;
        }

        public static CodeZeroIdentityBuilder AddCodeZeroRoleStore<TRoleStore>(this CodeZeroIdentityBuilder builder)
            where TRoleStore : class
        {
            var type = typeof(TRoleStore);
            var CodeZeroStoreType = typeof(CodeZeroRoleStore<,>).MakeGenericType(builder.RoleType, builder.UserType);
            var storeType = typeof(IRoleStore<>).MakeGenericType(builder.RoleType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(CodeZeroStoreType, services => services.GetRequiredService(type));
            builder.Services.AddScoped(storeType, services => services.GetRequiredService(type));
            return builder;
        }

        public static CodeZeroIdentityBuilder AddFeatureValueStore<TFeatureValueStore>(this CodeZeroIdentityBuilder builder)
            where TFeatureValueStore : class
        {
            var type = typeof(TFeatureValueStore);
            var storeType = typeof(CodeZeroFeatureValueStore<,>).MakeGenericType(builder.TenantType, builder.UserType);
            builder.Services.AddScoped(type);
            builder.Services.AddScoped(storeType, provider => provider.GetService(type));
            builder.Services.AddScoped(typeof(IFeatureValueStore), provider => provider.GetService(type));
            return builder;
        }
    }
}
