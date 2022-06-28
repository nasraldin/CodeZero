using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZero.Domain.Entities.Auditing;

/// <summary>
/// Implements <see cref="IFullAudited"/> 
/// to be a base class for full-audited entities.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TKey> : AuditedEntity<TKey>, IFullAudited
{
    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual string DeletedBy { get; set; } = default!;

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTime? DeletionTime { get; set; }
}

/// <summary>
/// Implements <see cref="IFullAudited{TUser, TKey}"/> 
/// to be a base class for full-audited entities.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
/// <typeparam name="TUser">Type of the user</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TKey, TUser> : AuditedEntity<TKey, TUser>, IFullAudited<TUser, TKey>
    where TUser : IEntity<TKey>
{
    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// Reference to the deleter user of this entity.
    /// </summary>
    [ForeignKey("DeletedBy")]
    public virtual TUser DeleterUser { get; set; } = default!;

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual string DeletedBy { get; set; } = default!;

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTime? DeletionTime { get; set; }
}