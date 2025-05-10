using App.WebAPI.Filters;

namespace App.WebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IServiceCollection AddContollersWithFiltersExt(this IServiceCollection services)
        {
            services.AddScoped(typeof(NotFoundFilter<,>));

            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            return services;
        }
    }
}
