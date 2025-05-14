using App.Application.Contracts.Caching;
using App.Caching;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace App.WebAPI.Extensions
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddCacheExt(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            return services;
        }
    }
}
