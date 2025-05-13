using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Queries;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure.Queries
{
    public class ProductQuery : IProductQuery
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<MongoModels.Product> _products;

        public ProductQuery(IMongoDatabase database)
        {
            _database = database;
            _products = database.GetCollection<MongoModels.Product>("Product");
        }

        public async Task<(List<Product> Products, int totalPages)> GetPaginatedProducts(int page, int amountPage, string loggedUserId)
        {
            // filter products differents in status pending
            var filter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("isDeleted", new BsonDocument("$ne", true)),
                new BsonDocument("status", new BsonDocument("$ne", Types.ProductStatus.Pending)),
                new BsonDocument("userId", new ObjectId(loggedUserId))
            });

            // total pages
            var totalRecords = await _products.CountDocumentsAsync(filter);
            var totalPages = (int)Math.Ceiling((double)totalRecords / amountPage);

            var pipeline = new[]
            {
                new BsonDocument("$match", filter),
                new BsonDocument("$sort", new BsonDocument("createdAt", -1)),
                new BsonDocument("$skip", (page - 1) * amountPage),
                new BsonDocument("$limit", amountPage),
                new BsonDocument("$project", new BsonDocument
                {
                    {"_id", 1 },
                    {"productPicture", 1 },
                    {"name", 1 },
                    {"quantity", 1 },
                    {"typeTranscription", 1 },
                    {"condition", 1 }
                })
            };

            var result = await _products.Aggregate<MongoModels.Product>(pipeline).ToListAsync();

            var productDomainResult = Mappers.ProductMapper.ToDomain(result);

            if (result == null || result.Count == 0) return (new List<Product>(), 0);

            return (productDomainResult, totalPages);
        }
    }
}
