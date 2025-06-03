using EcoTrueke.API.Requests.Person;
using EcoTrueke.API.Responses;
using EcoTrueke.API.Responses.Person;
using EcoTrueke.Domain.Interfaces.UseCases.Person;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {

        private readonly IPersonUseCase _personUseCase;

        public PersonController(IPersonUseCase personUseCase)
        {
            _personUseCase = personUseCase;
        }

        [HttpPut]
        public async Task<IActionResult> EditPerson([FromForm] EditPersonRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _personUseCase.EditPersonExecute(LoggedUserId, request.Name, request.PaternalSurname, request.MaternalSurname,
                    request.Phone, request.DocumentNumber, request.DocumentType, request.Address, request.Gender, request.ProfilePicture, request.ProfilePictureRemove);

                if (result.Data != null)
                {
                    var editPersonResponse = JsonConvert.DeserializeObject<EditPersonResponse>(result.Data);

                    var apiResponse = new ApiResponse<EditPersonResponse>(editPersonResponse!, result.Message);
                    return StatusCode(result.Code, apiResponse);
                }

                return StatusCode(result.Code, new { message = result.Message });

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("by-{userId}")]
        public async Task<IActionResult> GetPerson(string userId)
        {
            try
            {
                var result = await _personUseCase.GetPersonExecute(userId);

                if (result.Data != null)
                {
                    var getPersonResponse = JsonConvert.DeserializeObject<GetPersonResponse>(result.Data);

                    var apiResponse = new ApiResponse<GetPersonResponse>(getPersonResponse!, result.Message);

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
