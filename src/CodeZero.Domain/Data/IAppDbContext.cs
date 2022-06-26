namespace CodeZero.Domain.Data;

public interface IAppDbContext
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}