using Microsoft.Extensions.Configuration;
using StackExchange.Profiling.Storage;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Register the MiniProfiler middleware.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddMiniProfilerConfig(
        [NotNull] this IServiceCollection services,
        [NotNull] IConfiguration configuration)
    {
        var mpConfig = configuration.GetSection(nameof(MiniProfilerConfig)).Get<MiniProfilerConfig>() ?? new();

        // Note .AddMiniProfiler() returns a IMiniProfilerBuilder for easy intellisense
        services.AddMiniProfiler(options =>
        {
            // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

            // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
            options.RouteBasePath = mpConfig.RouteBasePath;

            // (Optional) Control storage
            // MemoryCache, MySql, SqlServer, Redis
            if (mpConfig is not null && !string.IsNullOrEmpty(mpConfig.Storage))
            {
                switch (mpConfig.Storage)
                {
                    case "Redis":
                        var conn = ConnectionMultiplexer.Connect(mpConfig.ConnectionString);
                        options.Storage = new RedisStorage(conn);
                        break;
                    case "MySql":
                        options.Storage = new MySqlStorage(mpConfig.ConnectionString);
                        break;
                    case "SqlServer":
                        options.Storage = new SqlServerStorage(mpConfig.ConnectionString);
                        break;
                    default:
                        (options.Storage as MemoryCacheStorage)!.CacheDuration = TimeSpan.FromMinutes(60);
                        break;
                }
            }
            else
            {
                // (default is 30 minutes in MemoryCacheStorage)
                // Note: MiniProfiler will not work if a SizeLimit is set on MemoryCache!
                //   See: https://github.com/MiniProfiler/dotnet/issues/501 for details
                (options.Storage as MemoryCacheStorage)!.CacheDuration = TimeSpan.FromMinutes(60);
            }

            // (Optional) Control which SQL formatter to use, InlineFormatter is the default
            options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

            // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
            // (defaults to true, and connection opening/closing is tracked)
            options.TrackConnectionOpenClose = true;

            // (Optional) Use something other than the "light" color scheme.
            // (defaults to "light")
            options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;

            // The below are newer options, available in .NET Core 3.0 and above:

            // (Optional) You can disable MVC filter profiling
            // (defaults to true, and filters are profiled)
            options.EnableMvcFilterProfiling = true;
            // ...or only save filters that take over a certain millisecond duration (including their children)
            // (defaults to null, and all filters are profiled)
            // options.MvcFilterMinimumSaveMs = 1.0m;

            // (Optional) You can disable MVC view profiling
            // (defaults to true, and views are profiled)
            options.EnableMvcViewProfiling = true;
            // ...or only save views that take over a certain millisecond duration (including their children)
            // (defaults to null, and all views are profiled)
            // options.MvcViewMinimumSaveMs = 1.0m;

            // (Optional) listen to any errors that occur within MiniProfiler itself
            // options.OnInternalError = e => MyExceptionLogger(e);

            // (Optional - not recommended) You can enable a heavy debug mode with stacks and tooltips when using memory storage
            // It has a lot of overhead vs. normal profiling and should only be used with that in mind
            // (defaults to false, debug/heavy mode is off)
            //options.EnableDebugMode = true;
        }).AddEntityFramework();

        return services;
    }
}