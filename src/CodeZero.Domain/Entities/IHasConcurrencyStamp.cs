namespace CodeZero.Domain.Entities;

/// <summary>
/// Concurrency control or management the consistency of the data 
/// when more than one user is accessing it for different purposes.
/// </summary>
public interface IHasConcurrencyStamp
{
    /// <summary>
    /// A random value that must change whenever a user is persisted to the store
    /// </summary>
    string ConcurrencyStamp { get; set; }
}