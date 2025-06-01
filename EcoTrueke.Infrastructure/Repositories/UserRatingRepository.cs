using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class UserRatingRepository : IUserRatingRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public UserRatingRepository(IDatabaseRepository databaseRepository)
        {
            this._databaseRepository = databaseRepository;
        }

        public async Task CreateUserRating(UserRating userRating)
        {
            // map data
            var mongoUserRating = UserRatingMapper.ToMongo(userRating);

            // insert user 
            await _databaseRepository.InsertOneAsync(mongoUserRating);
        }
    }
}
