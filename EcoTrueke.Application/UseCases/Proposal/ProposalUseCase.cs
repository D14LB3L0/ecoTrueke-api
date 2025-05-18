using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using EcoTrueke.Services.API;

namespace EcoTrueke.Application.UseCases.Proposal
{
    public class ProposalUseCase : IProposalUseCase
    {
        public Task<Result> RegisterProposalExecute(string proposerId, string ownerId, string proposalType, string offeredProductId, string requestedProductId)
        {
            throw new NotImplementedException();
        }
    }
}
