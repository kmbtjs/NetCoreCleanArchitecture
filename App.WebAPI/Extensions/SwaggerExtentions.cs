namespace App.WebAPI.Extensions
{
    public static class SwaggerExtentions
    {
        public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "App API",
                    Version = "v1"
                });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerGenExt(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
