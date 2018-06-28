//  <copyright file="Edition.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeZero.Domain.Entities.Auditing;
using CodeZero.MultiTenancy;

namespace CodeZero.Application.Editions
{
    /// <summary>
    /// Represents an edition of the application.
    /// </summary>
    [Table("CodeZeroEditions")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Edition : FullAuditedEntity
    {
        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Unique name of this edition.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Display name of this edition.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        public Edition()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        public Edition(string displayName)
            : this()
        {
            DisplayName = displayName;
        }
    }
}