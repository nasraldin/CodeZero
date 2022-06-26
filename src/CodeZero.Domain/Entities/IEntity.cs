namespace CodeZero.Domain.Entities;

/// <summary>
/// Defines an entity. It's primary key may not be "Id" or it may have a composite primary key.
/// Use <see cref="IEntity{TKey}"/> where possible for better integration 
/// to repositories and other structures in the framework.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Returns an array of ordered keys for this entity.
    /// </summary>
    /// <returns></returns>
    object[] GetKeys();
}

/// <summary>
/// Defines interface for base entity type. 
/// All entities in the system must implement this interface.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
public interface IEntity<out TKey> : IEntity
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    TKey Id { get; }

    /// <summary>
    /// Checks if this entity is transient 
    /// (not persisted to database and it has not an <see cref="Id"/>).
    /// </summary>
    /// <returns>True, if this entity is transient</returns>
    bool IsTransient();
}
