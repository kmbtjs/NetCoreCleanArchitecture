using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache, IDistributedCache distributedCache, IRedisCacheService redisCache) : ICacheService
    {
        public Task<T?> GetAsync<T>(string key)
        {
            return memoryCache.TryGetValue(key, out T? value) ? Task.FromResult(value) : Task.FromResult(default(T));
        }

        public Task<string?> GetAsync(string key)
        {
            //return distributedCache.GetStringAsync(key);
            return redisCache.GetAsync(key);
        }

        public Task<byte[]?> GetByteAsync(string key)
        {
            return distributedCache.GetAsync(key);
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

        public async Task AddAsync(string key, string value, TimeSpan? expirationTime = null)
        {
            //await distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = expirationTime
            //});

            await redisCache.SetStringAsync(key, value, expirationTime);
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            return distributedCache.RemoveAsync(key);
        }

        public void FlushAll()
        {
            redisCache.FlushAll();
        }
    }
}
