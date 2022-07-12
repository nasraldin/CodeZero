namespace CodeZero.Domain;

/// <summary>
/// Handling Concurrency Conflicts.
/// Timestamp/RowVersion
/// </summary>
public interface IHandleVersioned
{
    void HandleVersioned();
}
