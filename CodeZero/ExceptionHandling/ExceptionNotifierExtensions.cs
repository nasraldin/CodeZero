using CodeZero.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CodeZero.ExceptionHandling
{
    public static class ExceptionNotifierExtensions
    {
        public static Task NotifyAsync(
            [NotNull] this IExceptionNotifier exceptionNotifier,
            [NotNull] Exception exception,
            LogLevel? logLevel = null,
            bool handled = true)
        {
            Check.NotNull(exceptionNotifier, nameof(exceptionNotifier));

            return exceptionNotifier.NotifyAsync(
                new ExceptionNotificationContext(
                    exception,
                    logLevel,
                    handled
                )
            );
        }
    }
}