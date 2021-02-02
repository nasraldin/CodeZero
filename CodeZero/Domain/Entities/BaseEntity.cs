using CodeZero.Shared.Extensions.Collections;
using System;
using System.Collections.Generic;

namespace CodeZero.Domain.Entities
{
    /// <summary>
    /// Defines an entity. It's primary key may not be "Id" or it may have a composite primary key.
    /// Use <see cref="IEntity{TKey}"/> where possible for better integration to repositories and other structures in the framework.
    /// </summary>
    [Serializable]
    public abstract class BaseEntity : IEntity
    {
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Keys = {GetKeys().JoinAsString(", ")}";
        }

        public abstract object[] GetKeys();

        public bool EntityEquals(IEntity other)
        {
            return EntityHelper.EntityEquals(this, other);
        }
    }

    /// <summary>
    /// Basic implementation of IEntity interface.
    /// An entity can inherit this class of directly implement to IEntity interface.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class BaseEntity<TKey> : BaseEntity, IEntity<TKey>
    {
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();

        /// <inheritdoc />
        public virtual TKey Id { get; set; }

        protected BaseEntity()
        {
        }

        protected BaseEntity(TKey id)
        {
            Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }

        /// <inheritdoc />
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TKey>.Default.Equals(Id, default))
            {
                return true;
            }

            //Workaround for EF Core since it sets int / long to min value when attaching to dbcontext
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        private static bool IsTransient(BaseEntity<TKey> obj)
        {
            return obj != null && Equals(obj.Id, default(TKey));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity<TKey>);
        }

        public virtual bool Equals(BaseEntity<TKey> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (IsTransient(this) || IsTransient(other) || !Equals(Id, other.Id)) return false;
            var otherType = other.GetUnproxiedType();
            var thisType = GetUnproxiedType();
            return thisType.IsAssignableFrom(otherType) ||
                   otherType.IsAssignableFrom(thisType);
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity<TKey> x, BaseEntity<TKey> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity<TKey> x, BaseEntity<TKey> y)
        {
            return !(x == y);
        }
    }
}