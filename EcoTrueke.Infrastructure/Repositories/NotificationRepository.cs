using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public NotificationRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task<Notification> CreateNotification(Notification notification)
        {
            // map data
            var mongoNotification = NotificationMapper.ToMongo(notification);

            // insert person
            var resultMongoNotification = await _databaseRepository.InsertOneAsync(mongoNotification);

            // map data
            var domainNotification = NotificationMapper.ToDomain(resultMongoNotification);

            return domainNotification;
        }
    }
}
