using Application.Contract.Services;
using Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            
            return services;
        }
    }
}
