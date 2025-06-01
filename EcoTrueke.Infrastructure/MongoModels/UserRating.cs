using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EcoTrueke.Infrastructure.MongoModels
{
    public class UserRating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("rateduserId")]
        public string RatedUserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("qualifiedUserId")]
        public string QualifiedUserId { get; set; } 

        [BsonElement("stars")]
        public int Stars { get; set; } 

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
