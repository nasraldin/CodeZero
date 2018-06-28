//  <copyright file="EntityChange.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Domain.Entities;
using CodeZero.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZero.EntityHistory
{
    [Table("CodeZeroEntityChanges")]
    public class EntityChange : Entity<long>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of <see cref="EntityId"/> property.
        /// Value: 48.
        /// </summary>
        public const int MaxEntityIdLength = 48;

        /// <summary>
        /// Maximum length of <see cref="EntityTypeFullName"/> property.
        /// Value: 192.
        /// </summary>
        public const int MaxEntityTypeFullNameLength = 192;

        /// <summary>
        /// ChangeTime.
        /// </summary>
        public virtual DateTime ChangeTime { get; set; }

        /// <summary>
        /// ChangeType.
        /// </summary>
        public virtual EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// Gets/sets change set id, used to group entity changes.
        /// </summary>
        public virtual long EntityChangeSetId { get; set; }

        /// <summary>
        /// Gets/sets primary key of the entity.
        /// </summary>
        [MaxLength(MaxEntityIdLength)]
        public virtual string EntityId { get; set; }

        /// <summary>
        /// FullName of the entity type.
        /// </summary>
        [MaxLength(MaxEntityTypeFullNameLength)]
        public virtual string EntityTypeFullName { get; set; }

        /// <summary>
        /// TenantId.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// PropertyChanges.
        /// </summary>
        public virtual ICollection<EntityPropertyChange> PropertyChanges { get; set; }

        #region Not mapped

        [NotMapped]
        public virtual object EntityEntry { get; set; }

        #endregion
    }
}
