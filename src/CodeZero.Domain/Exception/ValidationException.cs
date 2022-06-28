using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using CodeZero.Exception;
using CodeZero.Logging;
using Microsoft.Extensions.Logging;

namespace CodeZero.Domain;

/// <summary>
/// This exception type is used to throws validation exceptions.
/// </summary>
[Serializable]
public class ValidationException : CodeZeroException,
        IHasLogLevel,
        IHasValidationErrors,
        IExceptionWithSelfLogging
{
    /// <summary>
    /// Detailed list of validation errors for this exception.
    /// </summary>
    public new IEnumerable<ValidationResult> ValidationErrors { get; }

    public IDictionary<string, string[]> Failures { get; } = default!;

    /// <summary>
    /// Exception severity.
    /// Default: Warn.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ValidationException()
    {
        ValidationErrors = new List<ValidationResult>();
        Failures = new Dictionary<string, string[]>();
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Constructor for serializing.
    /// </summary>
    public ValidationException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
        ValidationErrors = new List<ValidationResult>();
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    public ValidationException(string message)
        : base(message)
    {
        ValidationErrors = new List<ValidationResult>();
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="validationErrors">Validation errors</param>
    public ValidationException(IList<ValidationResult> validationErrors)
    {
        ValidationErrors = validationErrors;
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="validationErrors">Validation errors</param>
    public ValidationException(string message, IList<ValidationResult> validationErrors)
        : base(message)
    {
        ValidationErrors = validationErrors;
        LogLevel = LogLevel.Warning;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public ValidationException(string message, System.Exception innerException)
        : base(message, innerException)
    {
        ValidationErrors = new List<ValidationResult>();
        LogLevel = LogLevel.Warning;
    }

    public ValidationException(List<FluentValidation.Results.ValidationFailure> failures)
        : this()
    {
        var failureGroups = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        foreach (var failureGroup in failureGroups)
        {
            var propertyName = failureGroup.Key;
            var propertyFailures = failureGroup.ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }

    public void Log(ILogger logger)
    {
        if (ValidationErrors.IsNullOrEmpty())
        {
            return;
        }

        var validationErrors = new StringBuilder();
        validationErrors.AppendLine($"There are {ValidationErrors.Count()} validation errors:");

        foreach (var validationResult in ValidationErrors)
        {
            var memberNames = "";

            if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
            {
                memberNames = " (" + string.Join(", ", validationResult.MemberNames) + ")";
            }

            validationErrors.AppendLine(validationResult.ErrorMessage + memberNames);
        }

        logger.LogWithLevel(LogLevel, validationErrors.ToString());
    }
}
