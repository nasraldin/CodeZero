using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZero.Domain.Auditing
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>, IEntity<int>
    {
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TKey> : CreationAudited<TKey>, IAudited
    {
        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAudited{TUser}"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    /// <typeparam name="TUser">Type of the user</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TKey, TUser> : AuditedEntity<TKey>, IAudited<TUser>
        where TUser : IEntity<string>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        [ForeignKey("CreatedBy")]
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        [ForeignKey("UpdatedBy")]
        public virtual TUser ModifierUser { get; set; }
    }
}