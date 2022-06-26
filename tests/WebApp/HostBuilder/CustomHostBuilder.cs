using CodeZero;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;

namespace System;

/// <summary>
/// Custom Host Builder from <see cref="CodeZeroHostBuilder"/> 
/// with <see cref="ConfigurationBuilderOptions"/>.
/// </summary>
public static class CustomHostBuilder
{
    public static void CreateAsync(WebApplicationBuilder webApplication, string[] args)
    {
        // Example of configuring build options if you need that.
        var options = new ConfigurationBuilderOptions
        {
            BasePath = Directory.GetCurrentDirectory(),
            EnvironmentName = Environment.GetEnvironmentVariable(AppConsts.ASPNETCORE_ENVIRONMENT)!,
            CommandLineArgs = args,
            UserSecretsId = "c78a43fb-0c6c-4b2e-bac8-b0d721cb7eff" // this from <UserSecretsId> prop in CodeZeroTemplate.API.csproj
        };

        // Log Serilog Errors
        bool.TryParse(webApplication.Configuration["DebugConfig:SerilogSelfLog"], out bool result);
        if (result && options.EnvironmentName == AppConsts.Environments.Development)
        {
            SelfLog.Enable(Console.Error);
        }

        try
        {
            var builder = CodeZeroHostBuilder.CreateAsync(webApplication, options);

            // Add services to the container.
            // ...
            builder.Services.AddCodeZeroHealthChecks<DbContext>(builder.Configuration);

            var app = builder.Build();
            app.UseCodeZero(builder.Configuration);

            // Configure the HTTP request pipeline.
            // ...

            app.Run();
        }
        catch (Exception ex)
        {
            // Log.Logger will likely be internal type "Serilog.Core.Pipeline.SilentLogger".
            if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
            {
                // Loading configuration or Serilog failed.
                // This will create a logger that can be captured by Azure logger.
                // To enable Azure logger, in Azure Portal:
                // 1. Go to WebApp
                // 2. App Service logs
                // 3. Enable "Application Logging (Filesystem)", "Application Logging (Filesystem)" and "Detailed error messages"
                // 4. Set Retention Period (Days) to 10 or similar value
                // 5. Save settings
                // 6. Under Overview, restart web app
                // 7. Go to Log Stream and observe the logs
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            Log.Fatal(ex, "Host terminated unexpectedly.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Host terminated unexpectedly.");
            Console.WriteLine($"Exception: {ex}");
            Console.ResetColor();
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}