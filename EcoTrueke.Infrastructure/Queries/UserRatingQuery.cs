using EcoTrueke.Domain.DTOs;
using EcoTrueke.Domain.Interfaces.Queries;
using MongoDB.Driver;

namespace EcoTrueke.Infrastructure.Queries
{
    public class UserRatingQuery : IUserRatingQuery
    {

        private readonly IMongoCollection<MongoModels.User> _user;
        private readonly IMongoCollection<MongoModels.UserRating> _userRating;

        public UserRatingQuery(IMongoDatabase database)
        {
            _user = database.GetCollection<MongoModels.User>("User");
            _userRating = database.GetCollection<MongoModels.UserRating>("UserRating");
        }

        public Task<GetUserRatingResponse> GetUserRatingByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
