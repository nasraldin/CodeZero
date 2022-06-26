using FluentValidation.Results;

namespace CodeZero.Domain.Common.Responses;

public class BaseResponse
{
    public BaseResponse()
    {
        IsSuccess = false;
    }

    public BaseResponse(IEnumerable<ValidationFailure> failures)
    {
        IsSuccess = false;
        ValidationIssues = new List<ValidationIssue>();

        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            // Each PropertyName get's an array of failures associated with it:
            var PropertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            var propertyFailure = new ValidationIssue
            {
                PropertyName = propertyName,
                PropertyFailures = PropertyFailures.ToList()
            };

            ValidationIssues.Add(propertyFailure);
        }
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; } = default!;
    public IList<ValidationIssue> ValidationIssues { get; set; } = default!;
}
