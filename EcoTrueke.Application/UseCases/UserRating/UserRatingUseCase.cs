using EcoTrueke.Application.UseCases.Person;
using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.DTOs;
using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.UserRating;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.UserRating
{
    public class UserRatingUseCase : IUserRatingUseCase
    {

        private readonly IUserRatingRepository _userRatingRepository;
        private readonly IUserRatingQuery _userRatingQuery;

        public UserRatingUseCase(IUserRatingRepository userRatingRepository, IUserRatingQuery userRatingQuery)
        {
            _userRatingRepository = userRatingRepository;
            _userRatingQuery = userRatingQuery;
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

        public async Task<Result> GetUserRatingExecute(string userId)
        {
            try
            {
                var userRating = await _userRatingQuery.GetUserRatingByUserId(userId);

                return new Result { Code = Success.Person.GetPerson.Code, Data = JsonConvert.SerializeObject(userRating), Message = Success.Person.GetPerson.Message };


            }
            catch (Exception ex)
            {
            
                return Errors.UserRating.FailedGetUserRating;
            }
        }
    }
}
