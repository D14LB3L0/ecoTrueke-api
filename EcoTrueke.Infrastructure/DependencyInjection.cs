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
                .AddMongoDB(configuration);

            return services;    
        }

        private static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            var ecoTruekeDBSettings = new EcoTruekeDbSettings
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
    }
}
