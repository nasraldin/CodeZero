using System;

namespace CodeZero.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity{TKey}"/>.
    /// </summary>
    public interface IModification
    {
        /// <summary>
        /// Id of the modifier user of this entity.
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IModification"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IModification<TUser> : IModification where TUser : IEntity<string>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        TUser ModifierUser { get; set; }
    }
}
