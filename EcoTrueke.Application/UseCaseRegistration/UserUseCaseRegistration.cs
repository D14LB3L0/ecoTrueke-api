using EcoTrueke.Application.UseCases.Users;
using EcoTrueke.Domain.Interfaces.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class UserUseCaseRegistration
    {
        public static IServiceCollection AddUserUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            
            return services;
        }
    }
}
