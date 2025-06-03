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

        public async Task<Result> CreateUserRating(string ratedById, string qualifiedUserId, int stars)
        {

            try
            {
                var userRating = Domain.Entities.UserRating.CreateUserRating(ratedById, qualifiedUserId, stars);
                
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
