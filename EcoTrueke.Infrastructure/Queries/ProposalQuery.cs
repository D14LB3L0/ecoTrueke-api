using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Services.API;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure.Queries
{
    public class ProposalQuery : IProposalQuery
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<MongoModels.Proposal> _proposal;
        private readonly IMongoCollection<MongoModels.User> _user;
        private readonly IMongoCollection<MongoModels.Product> _product;

        public ProposalQuery(IMongoDatabase database)
        {
            _database = database;
            _proposal = database.GetCollection<MongoModels.Proposal>("Proposal");
            _user = database.GetCollection<MongoModels.User>("User");
            _product = database.GetCollection<MongoModels.Product>("Product");
        }
        public async Task<(List<object> Proposals, int totalPages)> GetProposals(int page, int amountPage, string loggedUserId)
        {
            var filter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("isDeleted", new BsonDocument("$ne", true)),
                new BsonDocument("userId", new ObjectId(loggedUserId))
            });

            // pipeline 
            var objectId = new ObjectId(loggedUserId);

            var pipeline = new[]
            {
               new BsonDocument("$match", filter),
               new BsonDocument("&lookup", new BsonDocument
               {
                   {"from", "User" },
                   {"localField", "proposerId" },
                   {"foreignField", "_id" },
                   {"as", "proposerUser" }
               }),

                new BsonDocument("$unwind", "$proposerUser"),

                new BsonDocument("$lookup", new BsonDocument
                {
                    {"from", "Product" },
                    {"localField", "offeredProductId" },
                    {"foreignField", "_id" },
                    {"as", "offeredProduct"}
                }),

                new BsonDocument("$unwind", "$offeredProduct"),

                new BsonDocument("$lookup", new BsonDocument
                {
                    {"from", "Product" },
                    {"localField", "requestedProductId" },
                    {"foreignField", "_id" },
                    {"as", "requestedProduct"}
                }),

                new BsonDocument("$unwind", "requestedProduct"),

                new BsonDocument("$facet", new BsonDocument
                {
                    { "data", new BsonArray
                        {
                            new BsonDocument("$skip", (page - 1) * amountPage),
                            new BsonDocument("$limit", amountPage)
                        }
                    },
                    { "count", new BsonArray
                        {
                            new BsonDocument("$count", "total")
                        }
                    }
                })
            };

            var result = await _proposal.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

            if (result == null || !result.Contains("data"))
            {
                return (new List<object>(), 0);
            }

            var results = new List<object>();

            foreach (var d in result["data"].AsBsonArray)
            {
                var dto = new
                {
                    Id = d["_id"].AsObjectId.ToString(),
                    ProposerId = d["proposerId"].AsObjectId.ToString(),
                    OwnerId = d["ownerId"].AsObjectId.ToString(),
                    ProposalType = d["proposalType"].AsString,
                    Status = d["status"].AsString,
                    CreatedAt = d["createdAt"].ToUniversalTime(),

                    ProposerUser = new
                    {
                        Id = d["proposerUser"]["_id"].AsObjectId.ToString(),
                        Name = d["proposerUser"]["name"].AsString
                    },
                    OfferedProduct = new
                    {
                        Id = d["offeredProduct"]["_id"].AsObjectId.ToString(),
                        Name = d["offeredProduct"]["name"].AsString
                    },
                    RequestedProduct = new
                    {
                        Id = d["requestedProduct"]["_id"].AsObjectId.ToString(),
                        Name = d["requestedProduct"]["name"].AsString
                    }
                };

                results.Add(dto);
            }
            
            // total pages
            var total = result["count"].AsBsonArray.FirstOrDefault()?["total"].ToInt32() ?? 0;
            var totalPages = (int)Math.Ceiling((double)total / amountPage);

            return (results, totalPages);

        }
    }
}
