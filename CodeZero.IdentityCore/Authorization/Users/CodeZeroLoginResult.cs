//  <copyright file="CodeZeroLoginResult.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Security.Claims;
using CodeZero.MultiTenancy;

namespace CodeZero.Authorization.Users
{
    public class CodeZeroLoginResult<TTenant, TUser>
        where TTenant : CodeZeroTenant<TUser>
        where TUser : CodeZeroUserBase
    {
        public CodeZeroLoginResultType Result { get; private set; }

        public TTenant Tenant { get; private set; }

        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public CodeZeroLoginResult(CodeZeroLoginResultType result, TTenant tenant = null, TUser user = null)
        {
            Result = result;
            Tenant = tenant;
            User = user;
        }

        public CodeZeroLoginResult(TTenant tenant, TUser user, ClaimsIdentity identity)
            : this(CodeZeroLoginResultType.Success, tenant)
        {
            User = user;
            Identity = identity;
        }
    }
}