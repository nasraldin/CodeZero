using System.Linq.Expressions;
using System.Reflection;
using CodeZero.Helpers;
using JetBrains.Annotations;

namespace CodeZero.Domain.Entities;

/// <summary>
/// Some helper methods for entities.
/// </summary>
public static class EntityHelper
{
    public static bool EntityEquals<TKey>(IEntity<TKey> entity1, IEntity<TKey> entity2)
    {
        if (entity1 is null || entity2 is null)
        {
            return false;
        }

        // Same instances must be considered as equal
        if (ReferenceEquals(entity1, entity2))
        {
            return true;
        }

        // Must have a IS-A relation of types or must be same type
        var typeOfEntity1 = entity1.GetType();
        var typeOfEntity2 = entity2.GetType();

        if (!typeOfEntity1.IsAssignableFrom(typeOfEntity2) &&
            !typeOfEntity2.IsAssignableFrom(typeOfEntity1))
        {
            return false;
        }

        // Transient objects are not considered as equal
        if (!IsDefaultKeyValue(entity1) && !IsDefaultKeyValue(entity2))
        {
            return false;
        }

        return true;
    }

    public static bool IsEntity([NotNull] Type type)
    {
        return typeof(IEntity<>).IsAssignableFrom(type);
    }

    public static bool IsEntityWithId([NotNull] Type type)
    {
        foreach (var interfaceType in type.GetInterfaces())
        {
            if (interfaceType.GetTypeInfo().IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
            {
                return true;
            }
        }

        return false;
    }

    public static bool HasDefaultId<TKey>(IEntity<TKey> entity)
    {
        if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
        {
            return true;
        }

        // Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
        if (typeof(TKey) == typeof(int))
        {
            return Convert.ToInt32(entity.Id) <= 0;
        }
        if (typeof(TKey) == typeof(long))
        {
            return Convert.ToInt64(entity.Id) <= 0;
        }

        return false;
    }

    private static bool IsDefaultKeyValue(object value)
    {
        if (value is null)
        {
            return true;
        }

        var type = value.GetType();

        // Workaround for EF Core since it sets int/long to min value when attaching to DbContext
        if (type == typeof(int))
        {
            return Convert.ToInt32(value) <= 0;
        }
        if (type == typeof(long))
        {
            return Convert.ToInt64(value) <= 0;
        }

        return TypeHelper.IsDefaultValue(value);
    }

    /// <summary>
    /// Tries to find the primary key type of the given entity type.
    /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
    /// </summary>
    [CanBeNull]
    public static Type FindPrimaryKeyType<TEntity>()
    {
        return FindPrimaryKeyType(typeof(TEntity));
    }

    /// <summary>
    /// Tries to find the primary key type of the given entity type.
    /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
    /// </summary>
    [CanBeNull]
    public static Type FindPrimaryKeyType([NotNull] Type entityType)
    {
        if (!typeof(IEntity<>).IsAssignableFrom(entityType))
        {
            throw new CodeZeroException($"Given {nameof(entityType)} is not an entity. It should implement {typeof(IEntity<>).AssemblyQualifiedName}!");
        }

        foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
        {
            if (interfaceType.GetTypeInfo().IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
            {
                return interfaceType.GenericTypeArguments[0];
            }
        }

        return null!;
    }

    public static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId<TEntity, TKey>(TKey id)
        where TEntity : IEntity<TKey>
    {
        var lambdaParam = Expression.Parameter(typeof(TEntity));
        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
        var idValue = Convert.ChangeType(id, typeof(TKey));
        Expression<Func<object>> closure = () => idValue!;
        var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);
        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }

    public static void TrySetId<TKey>(
        IEntity<TKey> entity,
        Func<TKey> idFactory,
        bool checkForDisableIdGenerationAttribute = false)
    {
        ObjectHelper.TrySetProperty(
            entity,
            x => x.Id,
            idFactory,
            checkForDisableIdGenerationAttribute
                ? new Type[] { typeof(DisableIdGenerationAttribute) }
                : Array.Empty<Type>());
    }
}

public class DisableIdGenerationAttribute : Attribute
{
}
