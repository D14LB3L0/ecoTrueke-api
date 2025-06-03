using EcoTrueke.Domain.DTOs;
using EcoTrueke.Domain.Interfaces.Queries;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure.Queries
{
    public class UserRatingQuery : IUserRatingQuery
    {

        private readonly IMongoCollection<MongoModels.UserRating> _userRating;

        public UserRatingQuery(IMongoDatabase database)
        {
            _userRating = database.GetCollection<MongoModels.UserRating>("UserRating");
        }

        public async Task<GetUserRatingResponse> GetUserRatingByUserId(string userId)
        {
            var objectId = ObjectId.Parse(userId);

            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "qualifiedUserId", objectId },
                    { "isDeleted", false }
                }),
                new BsonDocument("$group", new BsonDocument
                    {
                        { "_id", "$qualifiedUserId" },
                        { "averageStars", new BsonDocument("$avg", "$stars") }
                    })
                };

            var result = await _userRating.AggregateAsync<BsonDocument>(pipeline);
            var rating = await result.FirstOrDefaultAsync();

            return new GetUserRatingResponse(
                 rating != null ? rating["averageStars"].ToDouble() : 0
             );
        }

    }
}
