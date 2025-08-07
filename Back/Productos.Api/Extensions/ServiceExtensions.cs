using Productos.Common.Interface.Repository;
using Productos.Common.Interface.Service;
using Productos.Core.Services;
using Productos.Infrastructure.Context;
using Productos.Infrastructure.Repository;

namespace Productos.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IProductoService, ProductoService>();
            return services;
        }
    }
}
