using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public Task<T?> GetAsync<T>(string key)
        {
            return memoryCache.TryGetValue(key, out T value) ? Task.FromResult(value) : Task.FromResult(default(T));
        }
        public Task AddAsync<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };

            memoryCache.Set(key, value, cacheEntryOptions);

            return Task.CompletedTask;

        }
        public Task RemoveAsync(string key)
        {
            // Implementation for removing a value from the cache
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
