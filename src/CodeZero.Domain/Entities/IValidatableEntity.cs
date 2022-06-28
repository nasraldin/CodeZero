namespace CodeZero.Domain.Entities;

public interface IValidatableEntity
{
    /// <summary>
    /// Used to validation Entity.
    /// </summary>
    ValidateModelResult Validate();
}
