namespace CodeZero.Domain.Data;

public interface IAppDbContext
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

    void HandleAdded();
    void HandleModified();
    void HandleValidatable();
    void HandleVersioned();
    Task<bool> DispatchEvents();
}

//public class EntityTracker
//{
//}