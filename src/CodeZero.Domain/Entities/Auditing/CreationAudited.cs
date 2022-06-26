using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZero.Domain.Entities.Auditing;

/// <summary>
/// This class can be used to simplify implementing <see cref="ICreation"/>.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class CreationAudited<TKey> : BaseEntity<TKey>, ICreation
{
    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual string CreatedBy { get; set; } = default!;

    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    public virtual DateTime CreatedAt { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected CreationAudited()
    {
        CreatedAt = DateTime.Now;
    }
}

/// <summary>
/// This class can be used to simplify implementing <see cref="ICreation{TUser}"/>.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
/// <typeparam name="TUser">Type of the user</typeparam>
[Serializable]
public abstract class CreationAudited<TKey, TUser> : CreationAudited<TKey>, ICreation<TUser>
    where TUser : IEntity<string>
{
    /// <summary>
    /// Reference to the creator user of this entity.
    /// </summary>
    [ForeignKey("CreatedBy")]
    public virtual TUser CreatorUser { get; set; } = default!;
}