using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IProposalRepository
    {
        Task CreateExchangeProposal(Proposal proposal);

        Task<List<Proposal>> GetProposalsByUserId(string userId);

        Task<Proposal> GetProposalId(string proposalId);

        Task<List<Proposal>> GetProposalsByOwnerId(string userId);

        Task RejectOrAcceptProposal(string proposalId, string action);
    }
}
