using System.Reflection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add DataProtection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddDataProtectionConfig(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var dpConfig = configuration.GetSection(nameof(DataProtectionConfig)).Get<DataProtectionConfig>();
        var redisConfig = configuration.GetSection(nameof(RedisConfig)).Get<RedisConfig>();

        if (dpConfig is null)
        {
            throw new CodeZeroException($"Configure {nameof(DataProtectionConfig)} settings in appsettings.Environment.json");
        }

        // Remove any previously registered options setups.
        services.RemoveAll<IConfigureOptions<KeyManagementOptions>>();
        services.RemoveAll<IConfigureOptions<DataProtectionOptions>>();

        // The 'FileSystemXmlRepository' will create the directory, but only if it is not overridden.
        var directory = new DirectoryInfo(Path.Combine(dpConfig.Path, dpConfig.FileSystemDirectoryName));

        // Adds app level data protection services.
        var builder = services.AddDataProtection().SetApplicationName(Assembly.GetExecutingAssembly().FullName!);

        if (dpConfig.PersistKeysToRedis)
        {
            if (redisConfig is null)
            {
                throw new CodeZeroException($"Add your {nameof(RedisConfig)} to appsettings json.");
            }

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConfig.ConnectionString);
            builder.PersistKeysToStackExchangeRedis(redis, dpConfig.RedisKey);
        }
        else
        {
            builder.PersistKeysToFileSystem(directory)
            .AddKeyManagementOptions(options =>
            {
                options.NewKeyLifetime = TimeSpan.FromDays(dpConfig.KeyManagement.NewKeyLifetime);
                options.AutoGenerateKeys = dpConfig.KeyManagement.AutoGenerateKeys;
                options.XmlEncryptor ??= new NullXmlEncryptor();
            });
        }

        return services;
    }
}