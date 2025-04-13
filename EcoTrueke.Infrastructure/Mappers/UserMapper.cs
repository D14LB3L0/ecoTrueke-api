using EcoTrueke.Domain.Entities;
using EcoTrueke.Infrastructure.MongoModels;

namespace EcoTrueke.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static MongoModels.User ToMongo(Domain.Entities.User user)
        {
            return new MongoModels.User
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Email = user.Email,
                Password = user.Password,
                AccountStatus = user.AccountStatus,
                Subscription = user.Subscription,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsDeleted = user.IsDeleted
            };
        }

        public static Domain.Entities.User ToDomain(MongoModels.User mongoUser)
        {
            return new Domain.Entities.User
            {
                Id = mongoUser.Id,
                PersonId = mongoUser.PersonId,
                Email = mongoUser.Email,
                Password = mongoUser.Password,
                AccountStatus = mongoUser.AccountStatus,
                Subscription = mongoUser.Subscription,
                CreatedAt = mongoUser.CreatedAt,
                UpdatedAt = mongoUser.UpdatedAt,
                IsDeleted = mongoUser.IsDeleted
            };
        }
    }
}
