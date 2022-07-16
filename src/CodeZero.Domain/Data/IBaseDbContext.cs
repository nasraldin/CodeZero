namespace CodeZero.Domain.Data;

public interface IBaseDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}