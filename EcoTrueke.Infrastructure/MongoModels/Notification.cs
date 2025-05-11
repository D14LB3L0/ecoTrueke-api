using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EcoTrueke.Infrastructure.MongoModels
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("isRead")]
        public bool IsRead { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
