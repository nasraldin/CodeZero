//  <copyright file="MultiTenancyConsts.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.MultiTenancy
{
    public static class MultiTenancyConsts
    {
        /// <summary>
        /// Default tenant id: 1.
        /// </summary>
        public const int DefaultTenantId = 1;

        public const string TenantIdResolveKey = "CodeZero.TenantId";
    }
}