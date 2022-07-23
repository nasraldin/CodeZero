namespace CodeZero.Domain.Entities;

/// <summary>
/// Defines interface for base entity type. 
/// All entities in the system must implement this interface.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
public interface IEntity<out TKey>
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