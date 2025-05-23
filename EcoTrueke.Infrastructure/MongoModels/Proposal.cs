using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EcoTrueke.Infrastructure.MongoModels
{
    public class Proposal
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("proposerId")]
        public string ProposerId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ownerId")]
        public string OwnerId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("offeredProductId")]
        public string OfferedProductId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("requestedProductId")]
        public string RequestedProductId { get; set; }

        [BsonElement("proposalType")]
        public string ProposalType { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
