using AspNetCoreRateLimit;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CodeZero.RateLimit;

public class RedisRateLimitPolicyStore : IIpPolicyStore
{
    private readonly ILogger _logger;
    private readonly IIpPolicyStore _memoryCacheStore;
    //private readonly IConfig<RedisConfig> _redisOptions;
    private readonly ConnectionMultiplexer _redis;
    private readonly IpRateLimitOptions _options = default!;
    private readonly IpRateLimitPolicies _policies = default!;

    public RedisRateLimitPolicyStore(
        IConfig<RedisConfig> redisOptions,
        IMemoryCache memoryCache,
        ILogger<RedisRateLimitPolicyStore> logger,
        IConfig<IpRateLimitOptions> options = null!,
        IConfig<IpRateLimitPolicies> policies = null!)
    {
        _logger = logger;
        _memoryCacheStore = new MemoryCacheIpPolicyStore(memoryCache);
        //_redisOptions = redisOptions;
        _options = options?.Options!;
        _policies = policies?.Options!;
        _redis = ConnectionMultiplexer.Connect(redisOptions.Options.ConnectionString);
    }

    private IDatabase RedisDatabase => _redis.GetDatabase();

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await TryRedisCommandAsync(
            () =>
            {
                return RedisDatabase.KeyExistsAsync(id);
            },
            () =>
            {
                return _memoryCacheStore.ExistsAsync(id, cancellationToken);
            });
    }

    public async Task<IpRateLimitPolicies> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var command = await TryRedisCommandAsync(
            async () =>
            {
                var value = await RedisDatabase.StringGetAsync(id);

                if (!string.IsNullOrEmpty(value))
                {
                    return JsonSerializer.Deserialize<IpRateLimitPolicies>(value);
                }

                return null;
            },
            () =>
            {
                return _memoryCacheStore.GetAsync(id, cancellationToken);
            });

        return command!;
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _ = await TryRedisCommandAsync(
            async () =>
            {
                await RedisDatabase.KeyDeleteAsync(id);

                return true;
            },
            async () =>
            {
                await _memoryCacheStore.RemoveAsync(id, cancellationToken);

                return true;
            });
    }

    public async Task SeedAsync()
    {
        // on startup, save the IP rules defined in appsettings
        if (_options != null && _policies != null)
        {
            await SetAsync($"{_options.IpPolicyPrefix}", _policies).ConfigureAwait(false);
        }
    }

    public async Task SetAsync(string id, IpRateLimitPolicies entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _ = await TryRedisCommandAsync(
            async () =>
            {
                await RedisDatabase.StringSetAsync(id, JsonSerializer.Serialize(entry), expirationTime);

                return true;
            },
            async () =>
            {
                await _memoryCacheStore.SetAsync(id, entry, expirationTime, cancellationToken);

                return true;
            });
    }

    private async Task<T> TryRedisCommandAsync<T>(Func<Task<T>> command, Func<Task<T>> fallbackCommand)
    {
        bool.TryParse(AppServiceLoader.Instance.Configuration["ClientRateLimiting:EnableRateLimitingRedis"], out bool enableRateLimitingRedis);

        if (enableRateLimitingRedis && _redis?.IsConnected == true)
        {
            try
            {
                return await command();
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Redis command failed: {ex}");
            }
        }

        return await fallbackCommand();
    }
}