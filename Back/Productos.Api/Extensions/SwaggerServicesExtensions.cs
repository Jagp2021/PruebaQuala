using Microsoft.OpenApi.Models;

namespace Productos.Api.Extensions
{
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Productos",
                    Version = "v1",
                    Description = "API de Productos Quala"
                });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerGen(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1"));
            return app;
        }
    }
}
