using EcoTrueke.Application.UseCases.Person;
using EcoTrueke.Domain.Interfaces.UseCases.Person;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class PersonUseCaseRegistration
    {
        public static IServiceCollection addPersonUseCase(this IServiceCollection services)
        {
            services.AddScoped<IPersonUseCase, PersonUseCase>();

            return services;
        } 
    }
}
