//using Microsoft.Extensions.Caching.Distributed;

namespace CodeZero.Caching;

public interface ICacheProvider
{
    Task<T> GetFromCache<T>(string key) where T : class;
    //Task SetCache<T>(string key, T value, DistributedCacheEntryOptions options) where T : class;
    Task ClearCache(string key);
}