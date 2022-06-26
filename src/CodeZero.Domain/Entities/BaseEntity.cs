using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;

namespace CodeZero.Domain.Entities;

/// <summary>
/// Defines an entity. It's primary key may not be "Id" or 
/// it may have a composite primary key.
/// Use <see cref="IEntity{TKey}"/> where possible for better 
/// integration to repositories and other structures in the framework.
/// </summary>
[Serializable]
public abstract class Entity : IEntity
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
public abstract class BaseEntity<TKey> : Entity, IEntity<TKey>
{
    /// <inheritdoc />
    public virtual TKey Id { get; protected init; } = default!;

    protected BaseEntity() { }

    protected BaseEntity(TKey id) => Id = id;

    #region BaseBehaviours

    /// <inheritdoc />
    public virtual bool IsTransient()
    {
        if (EqualityComparer<TKey>.Default.Equals(Id, default))
        {
            return true;
        }

        // Workaround for EF Core since it sets int / long to min value when attaching to dbcontext
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
        return obj != null! && Equals(obj.Id, default(TKey));
    }

    public virtual bool _Equals(BaseEntity<TKey> other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (IsTransient(this) || IsTransient(other) || !Equals(Id, other.Id)) return false;

        var otherType = other.GetType();
        var thisType = GetType();

        return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
    }

    public override bool Equals(object? obj) => obj is BaseEntity<TKey> objS && _Equals(objS);

    public override int GetHashCode()
    {
        ArgumentNullException.ThrowIfNull(Id);

        if (Equals(Id, default(int)))
            return GetHashCode();

        return Id.GetHashCode();
    }

    public static bool operator ==(BaseEntity<TKey> x, BaseEntity<TKey> y) => Equals(x, y);

    public static bool operator !=(BaseEntity<TKey> x, BaseEntity<TKey> y)
    {
        return !(x == y);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }

    #endregion

    #region Validation

    [NotMapped]
    public bool IsValid => ValidationResult?.IsValid ?? Validate();

    [NotMapped]
    public ValidationResult ValidationResult { get; private set; } = default!;

    protected bool OnValidate<TValidator, TEntity>(TEntity entity, TValidator validator)
        where TValidator : AbstractValidator<TEntity>
        where TEntity : BaseEntity<TKey>
    {
        ValidationResult = validator.Validate(entity);

        return IsValid;
    }

    protected bool OnValidate<TValidator, TEntity>(TEntity entity, TValidator validator,
        Func<AbstractValidator<TEntity>, TEntity, ValidationResult> validation)
        where TValidator : AbstractValidator<TEntity>
        where TEntity : BaseEntity<TKey>
    {
        ValidationResult = validation(validator, entity);

        return IsValid;
    }

    protected void AddError(string errorMessage, ValidationResult validationResult = default!)
    {
        ValidationResult.Errors.Add(new(default, errorMessage));
        validationResult?.Errors.ToList().ForEach(failure => ValidationResult.Errors.Add(failure));
    }

    protected abstract bool Validate();

    #endregion
}