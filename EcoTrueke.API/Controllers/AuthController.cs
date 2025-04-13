using EcoTrueke.API.Requests.Auth;
using EcoTrueke.Domain.Interfaces.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;

        public AuthController(IRegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _registerUserUseCase.Execute(request.Name, request.PaternalSurname, request.MaternalSurname, request.Email, request.Password, request.ConfirmPassword);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
