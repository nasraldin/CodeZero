//  <copyright file="CodeZeroLoginManagerExtensions.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.MultiTenancy;
using CodeZero.Threading;

namespace CodeZero.Authorization
{
    public static class CodeZeroLogInManagerExtensions
    {
        public static CodeZeroLoginResult<TTenant, TUser> Login<TTenant, TRole, TUser>(
            this CodeZeroLogInManager<TTenant, TRole, TUser> logInManager, 
            string userNameOrEmailAddress, 
            string plainPassword, 
            string tenancyName = null, 
            bool shouldLockout = true)
                where TTenant : CodeZeroTenant<TUser>
                where TRole : CodeZeroRole<TUser>, new()
                where TUser : CodeZeroUser<TUser>
        {
            return AsyncHelper.RunSync(
                () => logInManager.LoginAsync(
                    userNameOrEmailAddress,
                    plainPassword,
                    tenancyName,
                    shouldLockout
                )
            );
        }
    }
}
