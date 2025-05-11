using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Infrastructure.Mappers
{
    public static class NotificationMapper
    {
        public static MongoModels.Notification ToMongo(Notification notification)
        {
            return new MongoModels.Notification
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title, 
                Message = notification.Message,
                Type = notification.Type,
                IsRead = notification.IsRead,
                Link = notification.Link,
                CreatedAt = notification.CreatedAt,
                IsDeleted = notification.IsDeleted,
                UpdatedAt = notification.UpdatedAt,
            };
        }

        public static Notification ToDomain(MongoModels.Notification mongoNotification)
        {
            return new Notification
            {
                Id = mongoNotification.Id,
                UserId = mongoNotification.UserId,
                Title = mongoNotification.Title,
                Message = mongoNotification.Message,
                Type = mongoNotification.Type,
                IsRead = mongoNotification.IsRead,
                Link = mongoNotification.Link,
                CreatedAt = mongoNotification.CreatedAt,
                IsDeleted = mongoNotification.IsDeleted,
                UpdatedAt = mongoNotification.UpdatedAt,
            };
        }

        public static List<Notification> ToDomain(List<MongoModels.Notification> mongoNotifications)
        {
            return mongoNotifications.Select(n => ToDomain(n)).ToList();
        }

    }
}
