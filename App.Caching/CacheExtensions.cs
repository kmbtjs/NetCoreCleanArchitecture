using App.Application.Contracts.Caching;
using App.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace App.Caching
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddCacheExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
            });

            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            //var redisConnection = ConnectionMultiplexer.Connect(connectionStrings!.Redis);

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionStrings!.Redis));
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            return services;
        }
    }
}
