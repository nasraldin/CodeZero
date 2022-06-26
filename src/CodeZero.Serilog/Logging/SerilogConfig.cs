using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace CodeZero.Logging;

public static class SerilogConfig
{
    public static void SetupLogger([NotNull] IConfiguration configuration)
    {
        Console.WriteLine($"[CodeZero] Loads Serilog Configuration...");

        var appName = configuration.GetSection("ApplicationName").Value ?? AppDomain.CurrentDomain.FriendlyName;

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", appName)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!)
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                                    .WithDefaultDestructurers()
                                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))

            // Used to filter out potentially bad data due debugging.
            // Very useful when doing Seq dashboards and want to remove logs under debugging session.
#if DEBUG
            .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached)
#endif
            .CreateLogger();
    }
}