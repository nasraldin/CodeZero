namespace CodeZero.Domain;

public class DomainException : CodeZeroException
{
    public DomainException() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, System.Exception innerException) : base(message, innerException) { }
}