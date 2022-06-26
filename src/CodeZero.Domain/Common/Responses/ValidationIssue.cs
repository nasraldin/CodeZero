namespace CodeZero.Domain.Common.Responses;

public class ValidationIssue
{
    public string PropertyName { get; set; } = default!;
    public List<string> PropertyFailures { get; set; } = default!;
}
