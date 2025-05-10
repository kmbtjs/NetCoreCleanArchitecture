using App.Application.Contracts.Caching;
using App.Caching;

namespace App.WebAPI.Extensions
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddCacheExt(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            //services.AddDistributedMemoryCache();
            //services.AddResponseCaching();
            return services;
        }
    }
}
