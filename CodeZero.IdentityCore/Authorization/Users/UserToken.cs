//  <copyright file="UserToken.cs" project="CodeZero.IdentityCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeZero.Domain.Entities;
using JetBrains.Annotations;

namespace CodeZero.Authorization.Users
{
    /// <summary>
    /// Represents an authentication token for a user.
    /// </summary>
    [Table("CodeZeroUserTokens")]
    public class UserToken : Entity<long>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of the <see cref="LoginProvider"/> property.
        /// </summary>
        public const int MaxLoginProviderLength = 64;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// Maximum length of the <see cref="Value"/> property.
        /// </summary>
        public const int MaxValueLength = 512;

        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        [StringLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        [StringLength(MaxValueLength)]
        public virtual string Value { get; set; }

        protected UserToken()
        {
            
        }

        protected internal UserToken(CodeZeroUserBase user, [NotNull] string loginProvider, [NotNull] string name, string value)
        {
            Check.NotNull(loginProvider, nameof(loginProvider));
            Check.NotNull(name, nameof(name));

            TenantId = user.TenantId;
            UserId = user.Id;
            LoginProvider = loginProvider;
            Name = name;
            Value = value;
        }
    }
}