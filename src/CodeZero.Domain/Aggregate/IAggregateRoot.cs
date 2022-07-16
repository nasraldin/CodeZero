namespace CodeZero.Domain.Aggregate;

/// <summary>
/// Marker interface to represent a aggregate root.
/// Apply this marker interface only to aggregate root entities
/// Repositories will only work with aggregate roots, not their children
/// </summary>
public interface IAggregateRoot { }