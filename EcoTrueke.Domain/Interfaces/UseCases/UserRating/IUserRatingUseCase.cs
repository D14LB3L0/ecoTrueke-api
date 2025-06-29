
using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.UserRating
{
    public interface IUserRatingUseCase
    {
        Task<Result> CreateUserRating(string ratedUserId, string qualifiedUserId , int stars, string proposalId);
        Task<Result> GetUserRatingExecute(string? userId = null, string? token = null);
    }
}
