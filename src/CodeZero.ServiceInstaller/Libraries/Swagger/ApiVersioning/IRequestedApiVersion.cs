namespace CodeZero.ApiVersioning;

public interface IRequestedApiVersion
{
    string Current { get; }
}