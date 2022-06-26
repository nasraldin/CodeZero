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
public interface IAudited<TUser> : IAudited, ICreation<TUser>, IModification<TUser>
    where TUser : IEntity<string>
{
}