using System.Text.Json;
using AspNetCoreRateLimit;
using CodeZero.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CodeZero.RateLimit;

public class RedisRateLimitCounterStore : IRateLimitCounterStore
{
    private readonly ILogger _logger;
    private readonly IRateLimitCounterStore _memoryCacheStore;
    //private readonly IConfig<RedisConfig> _redisOptions;
    private readonly ConnectionMultiplexer _redis;

    public RedisRateLimitCounterStore(
        IConfig<RedisConfig> redisOptions,
        IMemoryCache memoryCache,
        ILogger<RedisRateLimitCounterStore> logger)
    {
        _logger = logger;
        _memoryCacheStore = new MemoryCacheRateLimitCounterStore(memoryCache);
        IConfig<RedisConfig> _redisOptions = redisOptions;
        _redis = ConnectionMultiplexer.Connect(_redisOptions.Options.ConnectionString);
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

    public async Task<RateLimitCounter?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await TryRedisCommandAsync(
            async () =>
            {
                var value = await RedisDatabase.StringGetAsync(id);

                if (!string.IsNullOrEmpty(value))
                {
                    return JsonSerializer.Deserialize<RateLimitCounter?>(value);
                }

                return null;
            },
            () =>
            {
                return _memoryCacheStore.GetAsync(id, cancellationToken);
            });
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

    public async Task SetAsync(string id, RateLimitCounter? entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _ = await TryRedisCommandAsync(
            async () =>
            {
                if (entry.HasValue)
                    await RedisDatabase.StringSetAsync(id, JsonSerializer.Serialize(entry.Value), expirationTime);

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
        var serviceSettings = AppServiceLoader.Instance.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

        if (serviceSettings.EnableRateLimitingRedis && _redis?.IsConnected == true)
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