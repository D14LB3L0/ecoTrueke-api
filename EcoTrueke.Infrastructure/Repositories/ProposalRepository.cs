using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public ProposalRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task CreateExchangeProposal(Proposal proposal)
        {
            // map data
            var mongoProposal = ProposalMapper.ToMongo(proposal);

            // insert proposal
            await _databaseRepository.InsertOneAsync(mongoProposal);
        }

        public async Task<List<Proposal>> GetProposalsByUserId(string userId)
        {
            var mongoProposal = await _databaseRepository.FindManyAsync<MongoModels.Proposal>(
                p => p.ProposerId == userId && p.IsDeleted != true && p.Status == Types.ProposalStatus.Pending);

            // if the user doesn't exists
            if (mongoProposal == null)
                return null;

            return ProposalMapper.ToDomain(mongoProposal.ToList());
        }
    }
}
