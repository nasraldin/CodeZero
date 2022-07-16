namespace CodeZero.Domain.Exception;

public class DomainException : CodeZeroException
{
    public DomainException() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, System.Exception innerException)
        : base(message, innerException) { }
}