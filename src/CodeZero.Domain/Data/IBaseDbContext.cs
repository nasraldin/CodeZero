namespace CodeZero.Domain;

public interface IBaseDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
