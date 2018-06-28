//  <copyright file="UserIdentifierExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero
{
    /// <summary>
    /// Extension methods for <see cref="UserIdentifier"/> and <see cref="IUserIdentifier"/>.
    /// </summary>
    public static class UserIdentifierExtensions
    {
        /// <summary>
        /// Creates a new <see cref="UserIdentifier"/> object from any object implements <see cref="IUserIdentifier"/>.
        /// </summary>
        /// <param name="userIdentifier">User identifier.</param>
        public static UserIdentifier ToUserIdentifier(this IUserIdentifier userIdentifier)
        {
            return new UserIdentifier(userIdentifier.TenantId, userIdentifier.UserId);
        }
    }
}