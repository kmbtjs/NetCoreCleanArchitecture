using App.WebAPI.ExceptionHandlers;

namespace App.WebAPI.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
        {

            services.AddExceptionHandler<CriticalExceptionHandler>().AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
