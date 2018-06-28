//  <copyright file="IUserNavigationManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeZero.Application.Navigation
{
    /// <summary>
    /// Used to manage navigation for users.
    /// </summary>
    public interface IUserNavigationManager
    {
        /// <summary>
        /// Gets a menu specialized for given user.
        /// </summary>
        /// <param name="menuName">Unique name of the menu</param>
        /// <param name="user">The user, or null for anonymous users</param>
        Task<UserMenu> GetMenuAsync(string menuName, UserIdentifier user);

        /// <summary>
        /// Gets all menus specialized for given user.
        /// </summary>
        /// <param name="user">User id or null for anonymous users</param>
        Task<IReadOnlyList<UserMenu>> GetMenusAsync(UserIdentifier user);
    }
}