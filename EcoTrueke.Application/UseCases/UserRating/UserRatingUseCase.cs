using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.UserRating;
using EcoTrueke.Services.API;

namespace EcoTrueke.Application.UseCases.UserRating
{
    public class UserRatingUseCase : IUserRatingUseCase
    {

        private readonly IUserRatingRepository _userRatingRepository;

        public UserRatingUseCase(IUserRatingRepository userRatingRepository)
        {
            _userRatingRepository = userRatingRepository;
        }

        public async Task<Result> CreateUserRating(string ratedUserId, string qualifiedUserId, int stars, string proposalId)
        {

            try
            {
                var userRating = Domain.Entities.UserRating.CreateUserRating(ratedUserId, qualifiedUserId, stars, proposalId);
                
                await _userRatingRepository.CreateUserRating(userRating);

                return Success.UserRating.RegisterUserRating;
            }
            catch (Exception)
            {
                return Errors.UserRating.FailedRegisterUserRating;
            }
        }

        public Task<Result> GetUserRatingExecute(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
