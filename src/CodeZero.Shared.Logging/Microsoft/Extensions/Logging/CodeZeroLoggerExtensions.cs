using System.Text;
using CodeZero.Exception;
using CodeZero.Logging;
using JetBrains.Annotations;

namespace Microsoft.Extensions.Logging;

public static class CodeZeroLoggerExtensions
{
    public static void LogWithLevel(
        this ILogger logger,
        LogLevel logLevel,
        string message)
    {
        switch (logLevel)
        {
            case LogLevel.Information:
                logger.LogInformation(message);
                break;
            case LogLevel.Error:
                logger.LogError(message);
                break;
            case LogLevel.Warning:
                logger.LogWarning(message);
                break;
            case LogLevel.Critical:
                logger.LogCritical(message);
                break;
            case LogLevel.Trace:
                logger.LogTrace(message);
                break;
            default: // LogLevel.Debug || LogLevel.None
                logger.LogDebug(message);
                break;
        }
    }

    public static void LogWithLevel(
        this ILogger logger,
        LogLevel logLevel,
        string message,
        Exception exception)
    {
        switch (logLevel)
        {
            case LogLevel.Information:
                logger.LogInformation(message);
                break;
            case LogLevel.Error:
                logger.LogError(message);
                break;
            case LogLevel.Warning:
                logger.LogWarning(message);
                break;
            case LogLevel.Critical:
                logger.LogCritical(message);
                break;
            case LogLevel.Trace:
                logger.LogTrace(message);
                break;
            default: // LogLevel.Debug || LogLevel.None
                logger.LogDebug(message);
                break;
        }
    }

    public static void LogException(
        this ILogger logger,
        Exception ex,
        LogLevel? level = null)
    {
        var selectedLevel = level ?? ex.GetLogLevel();

        logger.LogWithLevel(selectedLevel, ex.Message, ex);
        LogKnownProperties(logger, ex, selectedLevel);
        LogSelfLogging(logger, ex);
        LogData(logger, ex, selectedLevel);
    }

    private static void LogKnownProperties(
        ILogger logger,
        Exception exception,
        LogLevel logLevel)
    {
        if (exception is IHasErrorCode exceptionWithErrorCode)
        {
            logger.LogWithLevel(logLevel, "Code: " + exceptionWithErrorCode.Code);
        }

        if (exception is IHasErrorDetails exceptionWithErrorDetails)
        {
            logger.LogWithLevel(logLevel, "Details: " + exceptionWithErrorDetails.Details);
        }
    }

    private static void LogData(
        ILogger logger,
        Exception exception,
        LogLevel logLevel)
    {
        if (exception.Data is null || exception.Data.Count <= 0)
        {
            return;
        }

        var exceptionData = new StringBuilder();
        exceptionData.AppendLine("---------- Exception Data ----------");

        foreach (var key in exception.Data.Keys)
        {
            exceptionData.AppendLine($"{key} = {exception.Data[key]}");
        }

        logger.LogWithLevel(logLevel, exceptionData.ToString());
    }

    private static void LogSelfLogging(ILogger logger, Exception exception)
    {
        var loggingExceptions = new List<IExceptionWithSelfLogging>();

        if (exception is IExceptionWithSelfLogging)
        {
            loggingExceptions.Add((exception as IExceptionWithSelfLogging)!);
        }
        else if (exception is AggregateException && exception.InnerException != null)
        {
            var aggException = exception as AggregateException;

            if (aggException?.InnerException is IExceptionWithSelfLogging)
            {
                loggingExceptions.Add((aggException?.InnerException as IExceptionWithSelfLogging)!);
            }

            foreach (var innerException in aggException?.InnerExceptions!)
            {
                if (innerException is IExceptionWithSelfLogging)
                {
                    loggingExceptions!.AddIfNotContains(innerException as IExceptionWithSelfLogging);
                }
            }
        }

        foreach (var ex in loggingExceptions)
        {
            ex.Log(logger);
        }
    }

    /// <summary>
    /// Adds an item to the collection if it's not already in the collection.
    /// </summary>
    /// <param name="source">The collection</param>
    /// <param name="item">Item to check and add</param>
    /// <typeparam name="T">Type of the items in the collection</typeparam>
    /// <returns>Returns True if added, returns False if not.</returns>
    private static bool AddIfNotContains<T>(
        [NotNull] this ICollection<T> source,
        T item)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (source.Contains(item))
        {
            return false;
        }

        source.Add(item);

        return true;
    }
}
