using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.UserRating
{
    public interface IUserRatingUseCase
    {
        Task<Result> GetUserRatingExecute(string userId);
    }
}
