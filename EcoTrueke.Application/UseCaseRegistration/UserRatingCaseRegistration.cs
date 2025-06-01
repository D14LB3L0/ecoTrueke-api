using EcoTrueke.Application.UseCases.UserRating;
using EcoTrueke.Domain.Interfaces.UseCases.UserRating;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class UserRatingCaseRegistration
    {
        public static IServiceCollection AddUserRatingUseCase(this IServiceCollection services)
        {
            services.AddScoped<IUserRatingUseCase, UserRatingUseCase>();

            return services;
        }
    }
}
