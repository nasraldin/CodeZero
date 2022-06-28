namespace CodeZero.Domain.Entities.Auditing;

/// <summary>
/// This interface is implemented by entities which wanted 
/// to store deletion information (who and when deleted).
/// <see cref="DeletionTime"/> is automatically set when deleting <see cref="IEntity{TKey}"/>.
/// </summary>
public interface IDeletion : ISoftDelete
{
    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    string DeletedBy { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    DateTime? DeletionTime { get; set; }
}

/// <summary>
/// Adds navigation properties to <see cref="IDeletion"/> interface for user.
/// </summary>
/// <typeparam name="TUser">Type of the user</typeparam>
/// <typeparam name="TKey">Type of the user primary key</typeparam>
public interface IDeletion<TUser, TKey> : IDeletion where TUser : IEntity<TKey>
{
    /// <summary>
    /// Reference to the deleter user of this entity.
    /// </summary>
    TUser DeleterUser { get; set; }
}