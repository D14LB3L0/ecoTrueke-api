using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EcoTrueke.Infrastructure.MongoModels
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("address")]
        public string Address { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("documentNumber")]
        public string DocumentNumber { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("documentType")]
        public string DocumentType { get; set; }    

        [BsonIgnoreIfNull]
        [BsonElement("gender")]
        public string Gender { get; set; }  

        [BsonIgnoreIfNull]
        [BsonElement("profilePicture")]
        public string ProfilePicture { get; set; }

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("creadtedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
