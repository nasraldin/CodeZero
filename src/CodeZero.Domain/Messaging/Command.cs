namespace CodeZero.Domain.Messaging;

public abstract class Command : Message, IRequest<ValidationResult>
{
    [System.ComponentModel.DataAnnotations.Timestamp]
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
        ValidationResult = new ValidationResult();
    }

    public virtual bool IsValid()
    {
        return ValidationResult.IsValid;
    }
}