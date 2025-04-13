using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EcoTrueke.Infrastructure.MongoModels
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("personId")]
        public string PersonId { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("accountStatus")]
        public string AccountStatus { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("subscription")]
        public string Subscription { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
