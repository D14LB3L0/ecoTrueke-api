using EcoTrueke.API.Requests.Proposal;
using EcoTrueke.API.Responses;
using EcoTrueke.API.Responses.Proposal;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProposalController : BaseController
    {
        private readonly IProposalUseCase _proposalUseCase;

        public ProposalController(IProposalUseCase proposalUseCase)
        {
            _proposalUseCase = proposalUseCase;
        }

        [HttpPost("exchange")]
        public async Task<IActionResult> ExchangeProposal([FromBody] ExchangeProposalRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _proposalUseCase.RegisterExchangeProposalExecute(LoggedUserId, request.OwnerId, request.ProposalType, request.OfferedProductId, request.RequestedProductId);

                return StatusCode(result.Code, new { message = result.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("requested")]
        public async Task<IActionResult> GetRequestedProposals()
        {
            try
            {
                var result = await _proposalUseCase.GetProposalsRequestedExecute(LoggedUserId);

                if (result.Data != null)
                {
                    var proposalRequestedResponse = JsonConvert.DeserializeObject<GetProposalsRequestedResponse>(result.Data);

                    var apiResponse = new ApiResponse<GetProposalsRequestedResponse>(proposalRequestedResponse, result.Message);

                    return StatusCode(result.Code, apiResponse);
                }

                return StatusCode(result.Code, new { message = result.Message });

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProposals([FromQuery] GetProposalsRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _proposalUseCase.GetProposalsExecute(request.Page, request.AmountPage,LoggedUserId);
                if (result.Data != null)
                {
                    var proposalResponse = JsonConvert.DeserializeObject<GetPaginatedProposalResponse>(result.Data);

                    var apiResponse = new ApiResponse<GetPaginatedProposalResponse>(proposalResponse, result.Message);

                    return StatusCode(result.Code, apiResponse);
                }

                return StatusCode(result.Code, new { message = result.Message });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
