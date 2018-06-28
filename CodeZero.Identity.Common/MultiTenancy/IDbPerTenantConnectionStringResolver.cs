//  <copyright file="IDbPerTenantConnectionStringResolver.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Domain.Uow;

namespace CodeZero.MultiTenancy
{
    /// <summary>
    /// Extends <see cref="IConnectionStringResolver"/> to
    /// get connection string for given tenant.
    /// </summary>
    public interface IDbPerTenantConnectionStringResolver : IConnectionStringResolver
    {
        /// <summary>
        /// Gets the connection string for given args.
        /// </summary>
        string GetNameOrConnectionString(DbPerTenantConnectionStringResolveArgs args);
    }
}
