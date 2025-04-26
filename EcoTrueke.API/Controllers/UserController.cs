using EcoTrueke.API.Requests.User;
using EcoTrueke.Domain.Interfaces.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpPatch]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _userUseCase.ChangePassword(LoggedUserId, request.Password);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

      
    }
}
