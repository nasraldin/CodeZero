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
public interface IFullAudited<TUser> : IAudited<TUser>, IFullAudited, IDeletion<TUser>
    where TUser : IEntity<string>
{
}