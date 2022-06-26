using System.ComponentModel.DataAnnotations;

namespace CodeZero.Exception;

public interface IHasValidationErrors
{
    IEnumerable<ValidationResult> ValidationErrors { get; }
}