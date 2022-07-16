using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// CodeZero Host Builder: A program initialization configuration parameters.
/// </summary>
public static class CodeZeroHostBuilder
{
    public static WebApplicationBuilder CreateAsync(
        WebApplicationBuilder webApplication,
        ConfigurationBuilderOptions options = null!)
    {
        var configuration = BuildConfiguration(options);
        webApplication.WebHost.UseConfiguration(configuration);
        webApplication.AddCodeZero();

        // Load ServiceSettings
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? new();
        var debugConfig = configuration.GetSection(nameof(DebugConfig)).Get<DebugConfig>() ?? new();

        webApplication.WebHost.UseKestrel(webHostBuilder =>
        {
            webHostBuilder.AddServerHeader = false;
        });
        webApplication.WebHost.CaptureStartupErrors(debugConfig.CaptureStartupErrors);
        webApplication.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey,
            debugConfig.DetailedErrorsKey.ToString().ToLower());

        // Add Serilog configuration
        if (serviceSettings.EnableSerilog)
        {
            webApplication.Host.UseSerilog();
            SerilogConfig.SetupLogger(configuration);
        }

        return webApplication;
    }

    public static IConfiguration BuildConfiguration(ConfigurationBuilderOptions options)
    {
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Assembly.GetExecutingAssembly().FullName?
            .Replace(".Builder", "")
            .Replace("Culture=neutral, PublicKeyToken=null", "by Nasr Aldin"));
        Console.ResetColor();
        Thread.CurrentThread.Name = $"{AppDomain.CurrentDomain.FriendlyName} Main thread";
        Console.WriteLine($"[CodeZero] CurrentThread: {Thread.CurrentThread.Name}");
        Console.WriteLine("[CodeZero] Build Configuration...");

        if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName($"{options.FileName}.{options.EnvironmentName}.json"))))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{options.FileName}.{options.EnvironmentName}.json FileNotFound");
            throw new FileNotFoundException($"{options.FileName}.{options.EnvironmentName}.json file is not existing. " +
                $"Please check your {options.FileName}.{options.EnvironmentName}.json is exist or create new one from appsettings.Template.json");
        }

        return ConfigurationHelper.BuildConfiguration(options);
    }
}