using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Proposal
{
    public interface IProposalUseCase
    {
        Task<Result> RegisterExchangeProposalExecute(string proposerId, string ownerId, string proposalType, string offeredProductId, string requestedProductId);

    }
}
