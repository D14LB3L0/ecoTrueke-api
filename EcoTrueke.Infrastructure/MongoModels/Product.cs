using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;

namespace EcoTrueke.Infrastructure.MongoModels
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("productPicture")]
        public string? ProductPicture { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("typeTranscription")]
        public string TypeTranscription { get; set; } // exchange - donation - sale

        [BsonElement("category")]
        public IEnumerable<string> Category { get; set; }  // clothes - toys

        [BsonElement("condition")]
        public string Condition { get; set; }  
        
        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } // pending - traded - sold - donnated

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
