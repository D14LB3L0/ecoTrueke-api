using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;

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
            await _databaseRepository.UpdateOneAsync<MongoModels.User>(
            u => u.Id == userId && u.IsDeleted != true,
            u => {
                u.UpdatedAt = DateTime.UtcNow;
                u.IsDeleted = true;
            });
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
        
        public async Task<Domain.Entities.User?> GetUserById(string userId)
        {
            // get mongo user
            var mongoUser = await _databaseRepository.FindOneAsync<MongoModels.User>(
              u => u.Id == userId && u.IsDeleted != true
            );

            // if the user doesn't exists
            if (mongoUser == null)
                return null;

            // map data to user
            return UserMapper.ToDomain(mongoUser);
        }

        public async Task ChangePassword(string userId, string newPasswordHash)
        {
            // update only password field
            await _databaseRepository.UpdateOneAsync<MongoModels.User>(
              u => u.Id == userId && u.IsDeleted != true,
               u => {
                   u.UpdatedAt = DateTime.UtcNow;
                   u.Password = newPasswordHash;
               });
        }

        public async Task UpdateUser(Domain.Entities.User user)
        {
            // map to mongo entity
            var mongoUser = UserMapper.ToMongo(user);

            // replace the document completely
            await _databaseRepository.ReplaceOneAsync(
                u => u.Id == mongoUser.Id && u.IsDeleted != true,
                mongoUser
            );
        }
    }
}
