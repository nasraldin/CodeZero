//  <copyright file="CodeZeroIdentityOwinAppBuilderExtensions.cs" project="CodeZero.Identity.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Users;
using CodeZero.Dependency;
using CodeZero.Extensions;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace CodeZero.Owin
{
    public static class CodeZeroIdentityOwinAppBuilderExtensions
    {
        public static void RegisterDataProtectionProvider(this IAppBuilder app)
        {
            if (!IocManager.Instance.IsRegistered<IUserTokenProviderAccessor>())
            {
                throw new CodeZeroException("IUserTokenProviderAccessor is not registered!");
            }

            var providerAccessor = IocManager.Instance.Resolve<IUserTokenProviderAccessor>();
            if (!(providerAccessor is OwinUserTokenProviderAccessor))
            {
                throw new CodeZeroException($"IUserTokenProviderAccessor should be instance of {nameof(OwinUserTokenProviderAccessor)}!");
            }

            providerAccessor.As<OwinUserTokenProviderAccessor>().DataProtectionProvider = app.GetDataProtectionProvider();
        }
    }
}