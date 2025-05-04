using EcoTrueke.Application.UseCases.Notification;
using EcoTrueke.Domain.Interfaces.UseCases.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class NotificationUseCaseRegistration
    {
        public static IServiceCollection AddNotificationUseCase(this IServiceCollection services)
        {
            services.AddScoped<INotificationUseCase, NotificationUseCase>();

            return services;
        }
    }
}
