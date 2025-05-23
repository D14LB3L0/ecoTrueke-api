using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Infrastructure.Communications;
using EcoTrueke.Infrastructure.Queries;
using EcoTrueke.Infrastructure.Repositories;
using EcoTrueke.Infrastructure.Security;
using EcoTrueke.Infrastructure.Upload;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMongoDB(configuration)
                .AddRepositories()
                .AddExternalServices()
                .AddQueries();

            return services;
        }

        private static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            var ecoTruekeDBSettings = new EcoTruekeDatabaseSettings
            {
                ConnectionString = configuration["ConnectionStrings:ConnectionString"],
                Database = configuration["ConnectionStrings:Database"]
            };

            services.AddSingleton<IMongoClient>(_ => new MongoClient(ecoTruekeDBSettings.ConnectionString));
            services.AddScoped(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(ecoTruekeDBSettings.Database);
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseRepository, MongoDatabaseRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProposalRepository, ProposalRepository>();

            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<INotificationQuery, NotificationQuery>();
            services.AddScoped<IProductQuery, ProductQuery>();
            services.AddScoped<IProposalQuery, ProposalQuery>();

            return services;
        }

        private static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMailerService, MailerService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
