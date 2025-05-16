using App.Application.Contracts.Caching;
using StackExchange.Redis;

namespace App.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public void FlushAll()
        {
            var redisEndpoints = _redisConnection.GetEndPoints(true);
            foreach (var redisEndpoint in redisEndpoints)
            {
                var redisServer = _redisConnection.GetServer(redisEndpoint);
                redisServer.FlushAllDatabases();
            }
        }

        public async Task<string?> GetAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetStringAsync(string key, string value, TimeSpan? expirationTime = null)
        {
            var db = _redisConnection.GetDatabase();
            var options = new TimeSpan?(expirationTime ?? TimeSpan.FromMinutes(5));
            await db.StringSetAsync(key, value, options);
        }
    }
}
