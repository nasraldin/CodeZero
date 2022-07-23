namespace CodeZero.Logging;

public static class HasLogLevelExtensions
{
    public static TException WithLogLevel<TException>(
        [NotNull] this TException exception,
        LogLevel logLevel)
        where TException : IHasLogLevel
    {
        ArgumentNullException.ThrowIfNull(exception);

        exception.LogLevel = logLevel;

        return exception;
    }
}