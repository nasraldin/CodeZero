using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeZero.Validation
{
    public interface IHasValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}