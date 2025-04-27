using EcoTrueke.Application.UseCaseRegistration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddAuthUseCases()
                .AddUserUseCases()
                .addPersonUseCase();

            return services;
        }
    }
}
