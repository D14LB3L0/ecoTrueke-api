using EcoTrueke.API.Requests.Proposal;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using Microsoft.AspNetCore.Mvc;

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
    }
}
