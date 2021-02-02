using System;

namespace CodeZero.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store creation information (who and when created).
    /// Creation time and creator user are automatically set when saving <see cref="BaseEntity{TKey}"/> to database.
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
    public interface ICreation<TUser> : ICreation where TUser : IEntity<string>
    {
        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        TUser CreatorUser { get; set; }
    }
}