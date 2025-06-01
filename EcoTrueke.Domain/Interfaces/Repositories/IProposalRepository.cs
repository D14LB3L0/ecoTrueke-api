using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IProposalRepository
    {
        Task CreateExchangeProposal(Proposal proposal);

        Task<List<Proposal>> GetProposalsByUserId(string userId);

        Task<Proposal> GetProposalById(string proposalId);

        Task<List<Proposal>> GetProposalsByOwnerId(string userId);

        Task UpdateStatusProposal(string proposalId, string action);
    }
}
