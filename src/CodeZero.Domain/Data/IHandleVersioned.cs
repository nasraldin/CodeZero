namespace CodeZero.Domain.Data;

/// <summary>
/// Handling Concurrency Conflicts.
/// Timestamp/RowVersion
/// </summary>
public interface IHandleVersioned
{
    void HandleVersioned();
}