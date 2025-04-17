using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;
using EcoTrueke.Infrastructure.MongoModels;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public UserRepository(IDatabaseRepository databaseRepository)
        {
            this._databaseRepository = databaseRepository;
        }

        public async Task<Domain.Entities.User> CreateUser(Domain.Entities.User user)
        {
            // map data
            var mongoUser = UserMapper.ToMongo(user);

            // insert user 
            var resultMongoUser = await _databaseRepository.InsertOneAsync(mongoUser);

            // map data 
            var domainUser = UserMapper.ToDomain(resultMongoUser);
            
            return domainUser;
        }

        public async Task DeleteUser(string userId)
        {
            // delete user
            await _databaseRepository.DeleteOneAsync<MongoModels.User>(u => u.Id == userId);
        }

        public async Task<Domain.Entities.User?> GetUserByEmail(string email)
        {
            // get mongo user
            var mongoUser = await _databaseRepository.FindOneAsync<MongoModels.User>(
                u => u.Email == email && u.IsDeleted != true
                );

            // if the user doesn't exists
            if (mongoUser == null)
                return null;

            // map data to user
            return UserMapper.ToDomain(mongoUser);
        }
    }
}
