using API.Filters;
using API.Mappings;
using API.Models.V1;
using FluentValidation;

namespace API
{
    public static class ConfigureService
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            // Add Exception filter
            services.AddControllers(opt => opt.Filters.Add<ApiExceptionFilterAttribute>());

            // Adding fluent validations
            services
                .AddScoped<IValidator<Order>, OrderValidator>()
                .AddScoped<IValidator<OrderItem>, OrderItemValidator>()
                .AddScoped<IValidator<Address>, AddressValidator>();

            // Add autoMapper profile
            services.AddAutoMapper((cfg) =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return services;
        }
    }
}
