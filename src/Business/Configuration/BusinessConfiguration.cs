using Business.Mappers;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Configuration;

public static class BusinessConfiguration
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        // services
        services.AddScoped<IProductService, ProductService>();

        // mappers
        services.AddScoped<ProductMapper>();

        return services;
    }
}
