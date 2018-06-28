//  <copyright file="FeatureSetting.cs" project="CodeZero.Identity.Common" solution="CodeZero">
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
using CodeZero.Domain.Entities.Auditing;
using CodeZero.MultiTenancy;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Base class for feature settings
    /// </summary>
    [Table("CodeZeroFeatures")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class FeatureSetting : CreationAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of the <see cref="Name"/> field.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// Maximum length of the <see cref="Value"/> property.
        /// </summary>
        public const int MaxValueLength = 2000;

        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Feature name.
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [MaxLength(MaxValueLength)]
        public virtual string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureSetting"/> class.
        /// </summary>
        protected FeatureSetting()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureSetting"/> class.
        /// </summary>
        /// <param name="name">Feature name.</param>
        /// <param name="value">Feature value.</param>
        protected FeatureSetting(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}