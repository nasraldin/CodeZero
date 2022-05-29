namespace CodeZero.Exception;

public interface IHasHttpStatusCode
{
    int HttpStatusCode { get; }
}