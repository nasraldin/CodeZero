//  <copyright file="IExternalAuthenticationSource.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.MultiTenancy;

namespace CodeZero.Authorization.Users
{
    /// <summary>
    /// Defines an external authorization source.
    /// </summary>
    /// <typeparam name="TTenant">Tenant type</typeparam>
    /// <typeparam name="TUser">User type</typeparam>
    public interface IExternalAuthenticationSource<TTenant, TUser>
        where TTenant : CodeZeroTenant<TUser>
        where TUser : CodeZeroUserBase
    {
        /// <summary>
        /// Unique name of the authentication source.
        /// This source name is set to <see cref="CodeZeroUserBase.AuthenticationSource"/>
        /// if the user authenticated by this authentication source
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Used to try authenticate a user by this source.
        /// </summary>
        /// <param name="userNameOrEmailAddress">User name or email address</param>
        /// <param name="plainPassword">Plain password of the user</param>
        /// <param name="tenant">Tenant of the user or null (if user is a host user)</param>
        /// <returns>True, indicates that this used has authenticated by this source</returns>
        Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, TTenant tenant);

        /// <summary>
        /// This method is a user authenticated by this source which does not exists yet.
        /// So, source should create the User and fill properties.
        /// </summary>
        /// <param name="userNameOrEmailAddress">User name or email address</param>
        /// <param name="tenant">Tenant of the user or null (if user is a host user)</param>
        /// <returns>Newly created user</returns>
        Task<TUser> CreateUserAsync(string userNameOrEmailAddress, TTenant tenant);

        /// <summary>
        /// This method is called after an existing user is authenticated by this source.
        /// It can be used to update some properties of the user by the source.
        /// </summary>
        /// <param name="user">The user that can be updated</param>
        /// <param name="tenant">Tenant of the user or null (if user is a host user)</param>
        Task UpdateUserAsync(TUser user, TTenant tenant);
    }
}