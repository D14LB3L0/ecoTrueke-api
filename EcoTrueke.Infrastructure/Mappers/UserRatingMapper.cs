using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Infrastructure.Mappers
{
    public static class UserRatingMapper
    {
        public static MongoModels.UserRating ToMongo(UserRating rating)
        {
            return new MongoModels.UserRating
            {
                Id = rating.Id,
                RatedUserId = rating.RatedUserId,
                QualifiedUserId = rating.QualifiedUserId,
                Stars = rating.Stars,
                ProposalId = rating.ProposalId,
                CreatedAt = rating.CreatedAt,
                UpdatedAt = rating.UpdatedAt,
                IsDeleted = false
            };
        }

        public static UserRating ToDomain(MongoModels.UserRating mongoRating)
        {
            return new UserRating
            {
                Id = mongoRating.Id,
                RatedUserId = mongoRating.RatedUserId,
                QualifiedUserId = mongoRating.QualifiedUserId,
                Stars = mongoRating.Stars,
                ProposalId = mongoRating.ProposalId,
                CreatedAt = mongoRating.CreatedAt,
                UpdatedAt = mongoRating.UpdatedAt,
                IsDeleted = false
            };
        }
    }
}
