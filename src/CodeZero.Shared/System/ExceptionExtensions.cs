using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System;

/// <summary>
/// Extension methods for <see cref="Exception"/> class.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception
    /// while preserving stack trace.
    /// </summary>
    /// <param name="exception">Exception to be re-thrown</param>
    public static void ReThrow(this Exception exception)
    {
        ExceptionDispatchInfo.Capture(exception).Throw();
    }

    public static bool IsFatal(this Exception ex)
    {
        return
            ex is OutOfMemoryException or
            SecurityException or
            SEHException;
    }
}