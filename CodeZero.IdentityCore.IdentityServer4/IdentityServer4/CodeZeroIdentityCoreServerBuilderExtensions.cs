//  <copyright file="CodeZeroIdentityCoreServerBuilderExtensions.cs" project="CodeZero.IdentityCore.IdentityServer4" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Users;
using CodeZero.IdentityServer4;
using CodeZero.Runtime.Security;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class CodeZeroIdentityCoreServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCodeZeroIdentityServer<TUser>(this IIdentityServerBuilder builder, Action<CodeZeroIdentityServerOptions> optionsAction = null)
            where TUser : CodeZeroUser<TUser>
        {
            var options = new CodeZeroIdentityServerOptions();
            optionsAction?.Invoke(options);

            builder.AddAspNetIdentity<TUser>();

            builder.AddProfileService<CodeZeroProfileService<TUser>>();
            builder.AddResourceOwnerValidator<CodeZeroResourceOwnerPasswordValidator<TUser>>();

            builder.Services.Replace(ServiceDescriptor.Transient<IClaimsService, CodeZeroClaimsService>());

            if (options.UpdateCodeZeroClaimTypes)
            {
                CodeZeroClaimTypes.UserId = JwtClaimTypes.Subject;
                CodeZeroClaimTypes.UserName = JwtClaimTypes.Name;
                CodeZeroClaimTypes.Role = JwtClaimTypes.Role;
            }

            if (options.UpdateJwtSecurityTokenHandlerDefaultInboundClaimTypeMap)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[CodeZeroClaimTypes.UserId] = CodeZeroClaimTypes.UserId;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[CodeZeroClaimTypes.UserName] = CodeZeroClaimTypes.UserName;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[CodeZeroClaimTypes.Role] = CodeZeroClaimTypes.Role;
            }

            return builder;
        }
    }
}
