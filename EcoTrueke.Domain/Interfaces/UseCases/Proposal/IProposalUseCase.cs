using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Proposal
{
    public interface IProposalUseCase
    {
        Task<Result> RegisterExchangeProposalExecute(string proposerId, string ownerId, string proposalType, string offeredProductId, string requestedProductId);

        Task<Result> GetProposalsRequestedExecute(string userId);

        Task<Result> GetProposalsExecute(int page, int amountPage, string? status, string loggedUserId);

        Task<Result> RejectOrAcceptProposalExecute(string proposalId, string action);

        Task<Result> ConfirmOrCancelProposalExecute(string proposalId, string actionProduct, string actionProposal);
    }
}
