namespace CodeZero.Logging;

public interface IExceptionWithSelfLogging
{
    void Log(ILogger logger);
}