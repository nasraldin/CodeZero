//  <copyright file="CodeZeroSessionExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="ICodeZeroSession"/>.
    /// </summary>
    public static class CodeZeroSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="CodeZeroException"/> if <see cref="ICodeZeroSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this ICodeZeroSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new CodeZeroException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// Gets current Tenant's Id.
        /// Throws <see cref="CodeZeroException"/> if <see cref="ICodeZeroSession.TenantId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current Tenant's Id.</returns>
        /// <exception cref="CodeZeroException"></exception>
        public static int GetTenantId(this ICodeZeroSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new CodeZeroException("Session.TenantId is null! Possible problems: No user logged in or current logged in user in a host user (TenantId is always null for host users).");
            }

            return session.TenantId.Value;
        }

        /// <summary>
        /// Creates <see cref="UserIdentifier"/> from given session.
        /// Returns null if <see cref="ICodeZeroSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">The session.</param>
        public static UserIdentifier ToUserIdentifier(this ICodeZeroSession session)
        {
            return session.UserId == null
                ? null
                : new UserIdentifier(session.TenantId, session.GetUserId());
        }
    }
}