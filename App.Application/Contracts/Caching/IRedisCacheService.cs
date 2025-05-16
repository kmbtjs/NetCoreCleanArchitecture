using StackExchange.Redis;

namespace App.Application.Contracts.Caching
{
    public interface IRedisCacheService
    {
        void FlushAll();
        Task<string?> GetAsync(string key);
        Task SetStringAsync(string key, string value, TimeSpan? expirationTime = null);
    }
}
