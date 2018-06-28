//  <copyright file="LoggerExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using Castle.Core.Logging;

namespace CodeZero.Logging
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, LogSeverity severity, string message)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.Fatal(message);
                    break;
                case LogSeverity.Error:
                    logger.Error(message);
                    break;
                case LogSeverity.Warn:
                    logger.Warn(message);
                    break;
                case LogSeverity.Info:
                    logger.Info(message);
                    break;
                case LogSeverity.Debug:
                    logger.Debug(message);
                    break;
                default:
                    throw new CodeZeroException("Unknown LogSeverity value: " + severity);
            }
        }

        public static void Log(this ILogger logger, LogSeverity severity, string message, Exception exception)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.Fatal(message, exception);
                    break;
                case LogSeverity.Error:
                    logger.Error(message, exception);
                    break;
                case LogSeverity.Warn:
                    logger.Warn(message, exception);
                    break;
                case LogSeverity.Info:
                    logger.Info(message, exception);
                    break;
                case LogSeverity.Debug:
                    logger.Debug(message, exception);
                    break;
                default:
                    throw new CodeZeroException("Unknown LogSeverity value: " + severity);
            }
        }

        public static void Log(this ILogger logger, LogSeverity severity, Func<string> messageFactory)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.Fatal(messageFactory);
                    break;
                case LogSeverity.Error:
                    logger.Error(messageFactory);
                    break;
                case LogSeverity.Warn:
                    logger.Warn(messageFactory);
                    break;
                case LogSeverity.Info:
                    logger.Info(messageFactory);
                    break;
                case LogSeverity.Debug:
                    logger.Debug(messageFactory);
                    break;
                default:
                    throw new CodeZeroException("Unknown LogSeverity value: " + severity);
            }
        }
    }
}