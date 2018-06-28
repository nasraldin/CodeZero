//  <copyright file="CodeZeroIdentityCoreServerBuilderEntityFrameworkCoreExtensions.cs" project="CodeZero.IdentityCore.IdentityServer4.EFCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace CodeZero.IdentityServer4
{
    public static class CodeZeroIdentityCoreServerBuilderEntityFrameworkCoreExtensions
    {
        public static IIdentityServerBuilder AddCodeZeroPersistedGrants<TDbContext>(this IIdentityServerBuilder builder)
            where TDbContext : ICodeZeroPersistedGrantDbContext
        {
            builder.Services.AddTransient<IPersistedGrantStore, CodeZeroPersistedGrantStore>();
            return builder;
        }
    }
}
