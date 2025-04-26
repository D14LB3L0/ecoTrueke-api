using EcoTrueke.Application.UseCases.User;
using EcoTrueke.Domain.Interfaces.UseCases.User;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class UserUseCaseRegistration
    {
        public static IServiceCollection AddUserUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUserUseCase, UserUseCase>();

            return services;
        }
    }
}
