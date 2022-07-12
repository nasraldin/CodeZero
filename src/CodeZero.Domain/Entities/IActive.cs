namespace CodeZero.Domain.Entities;

/// <summary>
/// Make an entity active/passive.
/// </summary>
public interface IActive
{
    /// <summary>
    /// True: This entity is active.
    /// False: This entity is not active.
    /// </summary>
    bool IsActive { get; }
}