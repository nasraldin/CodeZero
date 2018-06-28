//  <copyright file="AspNetCoreUserTokenProviderAccessor.cs" project="CodeZero.Identity.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.DataProtection;

namespace CodeZero.Identity.AspNetCore
{
    public class AspNetCoreUserTokenProviderAccessor : IUserTokenProviderAccessor
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public AspNetCoreUserTokenProviderAccessor(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public IUserTokenProvider<TUser, long> GetUserTokenProviderOrNull<TUser>()
            where TUser : CodeZeroUser<TUser>
        {
            return new DataProtectorUserTokenProvider<TUser>(_dataProtectionProvider.CreateProtector("ASP.NET Identity"));
        }
    }
}