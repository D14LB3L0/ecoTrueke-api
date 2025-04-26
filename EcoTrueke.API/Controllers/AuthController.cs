using EcoTrueke.API.Requests.Auth;
using EcoTrueke.API.Responses;
using EcoTrueke.API.Responses.Auth;
using EcoTrueke.Domain.Interfaces.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthUseCase _authUseCase;

        public AuthController(IAuthUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authUseCase.RegisterExecute(
                    request.Name, request.PaternalSurname, request.MaternalSurname, request.Email, request.Password, request.ConfirmPassword
                    );

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authUseCase.LoginExecute(request.Email, request.Password);

                if (result.Data != null)
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginUserResponse>(result.Data);

                    var apiResponse = new ApiResponse<LoginUserResponse>(loginResponse, result.Message);
                    return StatusCode(result.Code, apiResponse);

                }

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetUserPasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authUseCase.ResetPasswordExecute(request.Email);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var result = await _authUseCase.DeleteAccount(LoggedUserId);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
