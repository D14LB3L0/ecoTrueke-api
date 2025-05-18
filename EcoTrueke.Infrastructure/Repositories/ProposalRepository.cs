using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        public Task<Proposal> CreateUser(Proposal proposal)
        {
            throw new NotImplementedException();
        }
    }
}
