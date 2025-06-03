
using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.UserRating
{
    public interface IUserRatingUseCase
    {
        Task<Result> CreateUserRating(string ratedById, string qualifiedUserId, int stars);
        Task<Result> GetUserRatingExecute(string userId);
    }
}
