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

        public async Task<(List<Product> Products, int totalPages)> GetPaginatedProducts(int page, int amountPage, string loggedUserId, bool? myProducts = false, string? searchTerm = null)
        {

            var filters = new List<BsonDocument>
            {
                new BsonDocument("isDeleted", new BsonDocument("$ne", true)),
            };

            // filter products differents in status pendingx
            if(myProducts == true)
            {
                filters.Add(new BsonDocument("status", new BsonDocument("$in", new BsonArray
                {
                    Types.ProductStatus.Pending,
                    Types.ProductStatus.Active
                })));
                filters.Add(new BsonDocument("userId", new ObjectId(loggedUserId)));
            }

            if (myProducts == false)
            {
                if (!string.IsNullOrEmpty(loggedUserId))
                {
                    filters.Add(new BsonDocument("userId", new BsonDocument("$ne", new ObjectId(loggedUserId))));
                }

                filters.Add(new BsonDocument("status", new BsonDocument("$eq", Types.ProductStatus.Active)));
            }

            // search by name
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filters.Add(new BsonDocument("name", new BsonDocument
                {
                    { "$regex", searchTerm },
                    { "$options", "i" } 
                }));
            }

            // Final filter
            var filter = new BsonDocument("$and", new BsonArray(filters));

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
                    {"condition", 1 },
                    {"status", 1 }
                })
            };

            var result = await _products.Aggregate<MongoModels.Product>(pipeline).ToListAsync();

            var productDomainResult = Mappers.ProductMapper.ToDomain(result);

            if (result == null || result.Count == 0) return (new List<Product>(), 0);

            return (productDomainResult, totalPages);
        }
    }
}
