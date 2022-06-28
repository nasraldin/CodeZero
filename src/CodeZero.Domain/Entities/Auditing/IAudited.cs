namespace CodeZero.Domain.Entities.Auditing;

/// <summary>
/// This interface is implemented by entities which must be audited.
/// Related properties automatically set when saving/updating 
/// <see cref="IEntity{TKey}"/> objects.
/// </summary>
public interface IAudited : ICreation, IModification
{
}

/// <summary>
/// Adds navigation properties to <see cref="IAudited"/> interface for user.
/// </summary>
/// <typeparam name="TUser">Type of the user</typeparam>
/// <typeparam name="TKey">Type of the user primary key</typeparam>
public interface IAudited<TUser, TKey> : IAudited, ICreation<TUser, TKey>, IModification<TUser, TKey>
    where TUser : IEntity<TKey>
{
}