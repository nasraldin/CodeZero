using System;
using System.Collections.Generic;

namespace CodeZero.Domain
{
    /// <inheritdoc cref="Entity" />
    /// <summary>
    /// A shortcut of <see cref="Entity{TKey}"/> /> 
    /// for most used primary key type (<see cref="T:System.Int32" />).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {
    }

    /// <inheritdoc />
    /// <summary>
    /// Basic implementation of IEntity interface.
    /// An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        /// <inheritdoc />
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TKey>.Default.Equals(Id, default))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            //if (typeof(TKey) == typeof(int))
            //{
            //    return Convert.ToInt32(Id) <= 0;
            //}

            //if (typeof(TKey) == typeof(long))
            //{
            //    return Convert.ToInt64(Id) <= 0;
            //}

            return false;
        }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
