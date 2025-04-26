using EcoTrueke.Application.UseCases.Auth;
using EcoTrueke.Domain.Interfaces.UseCases.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class AuthUseCaseRegistration
    {
        public static IServiceCollection AddAuthUseCases(this IServiceCollection services)
        {
            services.AddScoped<IAuthUseCase, AuthUseCase>();
            
            return services;
        }
    }
}
