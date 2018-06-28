//  <copyright file="SessionOverride.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Runtime.Session
{
    public class SessionOverride
    {
        public long? UserId { get; }

        public int? TenantId { get; }

        public SessionOverride(int? tenantId, long? userId)
        {
            TenantId = tenantId;
            UserId = userId;
        }
    }
}