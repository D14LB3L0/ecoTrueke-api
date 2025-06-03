using EcoTrueke.API.Requests.UserRating;
using EcoTrueke.API.Responses;
using EcoTrueke.Domain.DTOs;
using EcoTrueke.Domain.Interfaces.UseCases.UserRating;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserRatingController : BaseController
    {
        private readonly IUserRatingUseCase _userRatingUseCase;
        public UserRatingController(IUserRatingUseCase userRatingUseCase)
        {
            _userRatingUseCase = userRatingUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> RatingUser([FromBody] RatingUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _userRatingUseCase.CreateUserRating(LoggedUserId, request.ProposerUserId, request.Stars, request.ProposalId);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRating(string userId)
        {
            try
            {
                var result = await _userRatingUseCase.GetUserRatingExecute(userId);

                if (result.Data != null)
                {
                    var getUserRatingResponse = JsonConvert.DeserializeObject<GetUserRatingResponse>(result.Data);

                    var apiResponse = new ApiResponse<GetUserRatingResponse>(getUserRatingResponse, result.Data);

                    return StatusCode(result.Code, apiResponse);
                }
                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
