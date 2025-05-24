using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.DTOs;
using EcoTrueke.Domain.Interfaces.Queries;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure.Queries
{
    public class ProposalQuery : IProposalQuery
    {
        private readonly IMongoCollection<MongoModels.Proposal> _proposal;
        private readonly IMongoCollection<MongoModels.User> _user;
        private readonly IMongoCollection<MongoModels.Product> _product;

        public ProposalQuery(IMongoDatabase database)
        {
            _proposal = database.GetCollection<MongoModels.Proposal>("Proposal");
            _user = database.GetCollection<MongoModels.User>("User");
            _product = database.GetCollection<MongoModels.Product>("Product");
        }
        public async Task<(List<GetProposalResponse> proposals, int totalPages)> GetProposals(int page, int amountPage, string loggedUserId)
        {
            var filter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("isDeleted", new BsonDocument("$ne", true)),
                new BsonDocument("ownerId", new ObjectId(loggedUserId)),
                new BsonDocument("status", Types.ProposalStatus.Pending)
            });

            // pipeline 

            var pipeline = new[]
            {
               new BsonDocument("$match", filter),
               new BsonDocument("$lookup", new BsonDocument
               {
                   {"from", "User" },
                   {"localField", "proposerId" },
                   {"foreignField", "_id" },
                   {"as", "proposerUser" }
               }),

                new BsonDocument("$unwind", "$proposerUser"),

                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "Person" },
                    { "localField", "proposerUser.personId" },
                    { "foreignField", "_id" },
                    { "as", "proposerPerson" }
                }),

                new BsonDocument("$unwind", "$proposerPerson"),

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

                new BsonDocument("$unwind", "$requestedProduct"),

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
                return (new List<GetProposalResponse>(), 0);
            }

            var results = new List<GetProposalResponse>();

            foreach (var d in result["data"].AsBsonArray)
            {
                var dto = new GetProposalResponse
                {
                    Id = d["_id"].AsObjectId.ToString(),
                    ProposerId = d["proposerId"].AsObjectId.ToString(),
                    OwnerId = d["ownerId"].AsObjectId.ToString(),
                    ProposalType = d["proposalType"].AsString,
                    Status = d["status"].AsString,
                    CreatedAt = d["createdAt"].ToUniversalTime(),

                    ProposerUser = new UserDto
                    {
                        Id = d["proposerUser"]["_id"].AsObjectId.ToString(),
                    },
                    ProposerPerson = new PersonDto
                    {
                        Id = d["proposerPerson"]["_id"].AsObjectId.ToString(),
                        Name = d["proposerPerson"]["name"].AsString
                    },
                    OfferedProduct = new ProductDto
                    {
                        Id = d["offeredProduct"]["_id"].AsObjectId.ToString(),
                        Name = d["offeredProduct"]["name"].AsString,
                        ProductPicture = d["offeredProduct"]["productPicture"].AsString
                    },
                    RequestedProduct = new ProductDto
                    {
                        Id = d["requestedProduct"]["_id"].AsObjectId.ToString(),
                        Name = d["requestedProduct"]["name"].AsString,
                        ProductPicture = d["requestedProduct"]["productPicture"].AsString
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
