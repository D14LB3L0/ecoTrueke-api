using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IProposalRepository
    {
        Task CreateExchangeProposal(Proposal proposal);
    }
}
