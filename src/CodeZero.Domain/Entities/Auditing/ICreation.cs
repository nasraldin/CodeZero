namespace CodeZero.Domain.Entities.Auditing;

/// <summary>
/// This interface is implemented by entities that is wanted 
/// to store creation information (who and when created).
/// Creation time and creator user are automatically 
/// set when saving <see cref="IEntity{TKey}"/> to database.
/// </summary>
public interface ICreation
{
    /// <summary>
    /// Id of the creator user of this entity.
    /// </summary>
    string CreatedBy { get; set; }

    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    DateTime CreatedAt { get; set; }
}

/// <summary>
/// Adds navigation properties to <see cref="ICreation"/> interface for user.
/// </summary>
/// <typeparam name="TUser">Type of the user</typeparam>
/// <typeparam name="TKey">Type of the user primary key</typeparam>
public interface ICreation<TUser, TKey> : ICreation where TUser : IEntity<TKey>
{
    /// <summary>
    /// Reference to the creator user of this entity.
    /// </summary>
    TUser CreatorUser { get; set; }
}