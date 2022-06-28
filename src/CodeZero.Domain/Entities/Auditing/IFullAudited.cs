namespace CodeZero.Domain.Entities.Auditing;

/// <summary>
/// This interface ads <see cref="IDeletion"/> 
/// to <see cref="IAudited"/> for a fully audited entity.
/// </summary>
public interface IFullAudited : IAudited, IDeletion
{
}

/// <summary>
/// Adds navigation properties to <see cref="IFullAudited"/> interface for user.
/// </summary>
/// <typeparam name="TUser">Type of the user</typeparam>
/// <typeparam name="TKey">Type of the user primary key</typeparam>
public interface IFullAudited<TUser, TKey> : IAudited<TUser, TKey>, IFullAudited, IDeletion<TUser, TKey>
    where TUser : IEntity<TKey>
{
}