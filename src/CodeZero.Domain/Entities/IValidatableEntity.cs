namespace CodeZero.Domain.Entities;

/// <summary>
/// Interface for all classes that may be validated on their own.
/// </summary>
public interface IValidatableEntity
{
    /// <summary>
    /// Validate the entity, returning a validation result.
    /// </summary>
    /// <returns>A validation result containing errors - or not.</returns>
    ValidationResult ValidationResult { get; }

    /// <summary>
    /// Validate the entity.
    /// </summary>
    /// <returns>A validation result true or false.</returns>
    bool Validate();
}