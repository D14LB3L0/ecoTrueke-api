using EcoTrueke.API.Requests.UserRating;
using EcoTrueke.Domain.Interfaces.UseCases.UserRating;
using Microsoft.AspNetCore.Mvc;

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

                var result = await _userRatingUseCase.CreateUserRating(LoggedUserId, request.QualifiedUserId, request.Stars);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
