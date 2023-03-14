using Application.Contract.Repository;
using Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Singleton to use as a in-memory database with stub data within the OrderRepository class
            services.AddSingleton<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}