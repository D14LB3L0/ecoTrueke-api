using EcoTrueke.Application.UseCases.Product;
using EcoTrueke.Domain.Interfaces.UseCases.Product;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class ProductUseCaseRegistration
    {
        public static IServiceCollection AddProductUseCases(this IServiceCollection services)
        {
            services.AddScoped<IProductUseCase, ProductUseCase>();

            return services;
        }
    }
}
