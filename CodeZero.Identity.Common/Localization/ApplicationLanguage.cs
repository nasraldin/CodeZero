//  <copyright file="ApplicationLanguage.cs" project="CodeZero.Identity.Common" solution="CodeZero">
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
using CodeZero.Domain.Entities;
using CodeZero.Domain.Entities.Auditing;

namespace CodeZero.Localization
{
    /// <summary>
    /// Represents a language of the application.
    /// </summary>
    [Serializable]
    [Table("CodeZeroLanguages")]
    public class ApplicationLanguage : FullAuditedEntity, IMayHaveTenant
    {
        /// <summary>
        /// The maximum name length.
        /// </summary>
        public const int MaxNameLength = 10;

        /// <summary>
        /// The maximum display name length.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// The maximum icon length.
        /// </summary>
        public const int MaxIconLength = 128;

        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the culture, like "en" or "en-US".
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        [StringLength(MaxIconLength)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// Is this language active. Inactive languages are not get by <see cref="IApplicationLanguageManager"/>.
        /// </summary>
        public virtual bool IsDisabled { get; set; }

        /// <summary>
        /// Creates a new <see cref="ApplicationLanguage"/> object.
        /// </summary>
        public ApplicationLanguage()
        {
        }

        public ApplicationLanguage(int? tenantId, string name, string displayName, string icon = null, bool isDisabled = false)
        {
            TenantId = tenantId;
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            IsDisabled = isDisabled;
        }

        public virtual LanguageInfo ToLanguageInfo()
        {
            return new LanguageInfo(Name, DisplayName, Icon, isDisabled: IsDisabled);
        }
    }
}
