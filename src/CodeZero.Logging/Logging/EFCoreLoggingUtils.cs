using Microsoft.Extensions.Logging;

namespace CodeZero.Logging;

public static class EFCoreLoggingUtils
{
    public static IDisposable EFQueryScope<T>(this ILogger<T> logger, string queryScopeName)
    {
        return logger.BeginScope(new Dictionary<string, object> { { "EFQueries", queryScopeName } });
    }

    public static IDisposable EFQueryScope(this ILogger logger, string queryScopeName)
    {
        return logger.BeginScope(new Dictionary<string, object> { { "EFQueries", queryScopeName } });
    }
}