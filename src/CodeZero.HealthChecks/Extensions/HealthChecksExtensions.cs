using CodeZero.Configuration;
using CodeZero.HealthChecks.Configuration;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add health checks.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <param name="dbContextName"></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCodeZeroHealthChecks<TContext>(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration,
        string dbContextName = "ApplicationDbContext") where TContext : DbContext
    {
        var hcsUIConfig = configuration.GetSection(nameof(HealthChecksUI)).Get<HealthChecksUI>();

        // Registers required services for health checks
        var healthChecks = services.AddHealthChecks();

        if (hcsUIConfig.Checks.DbContext)
        {
            healthChecks
                .AddDbContextCheck<TContext>(dbContextName, tags: new[] { "DB" });
        }

        if (hcsUIConfig.Checks.Database)
        {
            var dbConfig = configuration.GetSection(nameof(MariaDB)).Get<MariaDB>();
            healthChecks
                .AddMySql(dbConfig.DefaultConnection, "MariaDB Server", tags: new[] { "DB" });
        }

        if (hcsUIConfig.Checks.Redis)
        {
            var redisConfig = configuration.GetSection(nameof(RedisConfig)).Get<RedisConfig>();
            healthChecks.AddRedis(redisConfig.ConnectionString, "Redis Server", tags: new[] { "Cache" });
        }

        if (hcsUIConfig.Checks.Seq)
        {
            var seqOptions = configuration.GetSection(nameof(SeqOptions)).Get<SeqOptions>();
            healthChecks.AddSeqPublisher(seq => { seq.Endpoint = seqOptions.Endpoint; }, "HealthChecks");
        }

        var healthChecksUI = services.AddHealthChecksUI(opt =>
        {
            opt.SetHeaderText(hcsUIConfig.HeaderText);
        });

        if (hcsUIConfig.EnableDatabaseStorage)
        {
            healthChecksUI.AddMySqlStorage(hcsUIConfig.StorageConnectionString);
        }
        else
        {
            healthChecksUI.AddInMemoryStorage();
        }

        return services;
    }
}
