using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Queries;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure.Queries
{
    public class NotificationQuery : INotificationQuery
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<MongoModels.Notification> _notifications;

        public NotificationQuery(IMongoDatabase database)
        {
            _database = database;
            _notifications = database.GetCollection<MongoModels.Notification>("Notification"); ;
        }

        public async Task<(List<Notification> Notifications, int totalPages)> GetPaginatedNotifications(int page, int amountPage, string loggedUserId)
        {
            // filter notification read
            var filter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("isRead", new BsonDocument("$ne", true)),
                new BsonDocument("userId", new ObjectId(loggedUserId))
            });

            // total pages
            var totalRecords = await _notifications.CountDocumentsAsync(filter);
            var totalPages = (int)Math.Ceiling((double)totalRecords / amountPage);

            var pipeline = new[]
            {
                new BsonDocument("$match", filter),
                new BsonDocument("$sort", new BsonDocument("createdAt", -1)),
                new BsonDocument("$skip", (page - 1) * amountPage),
                new BsonDocument("$limit", amountPage),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 1 },
                    { "title", 1 },
                    { "message", 1 },
                    { "type", 1 }
                })

            };

            var result = await _notifications.Aggregate<MongoModels.Notification>(pipeline).ToListAsync();

            var notificationDomainResult = Mappers.NotificationMapper.ToDomain(result);

            if (result == null || result.Count == 0) return (new List<Notification>(), 0);

            return (notificationDomainResult, totalPages);
        }
    };
}
