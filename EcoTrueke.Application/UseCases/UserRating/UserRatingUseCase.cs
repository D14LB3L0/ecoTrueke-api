using EcoTrueke.Domain.Interfaces.UseCases.UserRating;
using EcoTrueke.Services.API;

namespace EcoTrueke.Application.UseCases.UserRating
{
    public class UserRatingUseCase : IUserRatingUseCase
    {
        public Task<Result> CreateUserRatingExecute(string ratedById, string qualifiedUserId, int stars)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetUserRatingExecute(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
