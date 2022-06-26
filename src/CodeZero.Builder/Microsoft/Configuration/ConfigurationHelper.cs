using CodeZero;

namespace Microsoft.Extensions.Configuration;

public static class ConfigurationHelper
{
    public static IConfigurationRoot BuildConfiguration(
        ConfigurationBuilderOptions options = null!,
        Action<IConfigurationBuilder> builderAction = null!)
    {
        options ??= new ConfigurationBuilderOptions();

        if (string.IsNullOrEmpty(options.BasePath))
        {
            options.BasePath = Directory.GetCurrentDirectory();
        }

        if (string.IsNullOrEmpty(options.EnvironmentName))
        {
            options.EnvironmentName = Environment.GetEnvironmentVariable(AppConsts.ASPNETCORE_ENVIRONMENT)!;
        }

        Console.WriteLine("[CodeZero] Loads environment variables...");
        var builder = new ConfigurationBuilder()
            .SetBasePath(options.BasePath)
            .AddJsonFile($"{options.FileName}.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{options.FileName}.{options.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{options.FileName}.{options.SerilogFileName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{options.FileName}.{options.LanguageFileName}.json", optional: true, reloadOnChange: true);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[CodeZero] Environment variables loaded.");
        Console.ResetColor();

        if ((options.EnvironmentName == AppConsts.Environments.Development ||
            options.EnvironmentName == AppConsts.Environments.Dev) &&
            options.UserSecretsId is not null)
        {
            Console.WriteLine("[CodeZero] Check UserSecrets...");
            builder.AddUserSecrets(options.UserSecretsId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[CodeZero] UserSecrets loaded.");
        }

        if (!string.IsNullOrEmpty(options.EnvironmentVariablesPrefix))
        {
            builder.AddEnvironmentVariables(options.EnvironmentVariablesPrefix);
        }

        if (options.CommandLineArgs != null)
        {
            builder.AddCommandLine(options.CommandLineArgs);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[CodeZero] Configuration Build Done!.");
        Console.ResetColor();

        builderAction?.Invoke(builder);

        return builder.Build();
    }
}