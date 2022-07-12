namespace CodeZero.Domain;

/// <summary>
/// Handle Change Tracking in EF Core
/// </summary>
public interface IEntityTracker : IHandleCreation, IHandleModification, IHandleDeletion
{
}
