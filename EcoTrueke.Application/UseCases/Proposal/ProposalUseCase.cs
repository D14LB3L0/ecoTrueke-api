using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using EcoTrueke.Services.API;

namespace EcoTrueke.Application.UseCases.Proposal
{
    public class ProposalUseCase : IProposalUseCase
    {

        private readonly IProposalRepository _proposalRepository;

        public ProposalUseCase(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public async Task<Result> RegisterExchangeProposalExecute(string proposerId, string ownerId, string proposalType, string offeredProductId, string requestedProductId)
        {
            // create exchange proposal
            var exchangeProposal = Domain.Entities.Proposal.Create(proposerId, ownerId, proposalType, offeredProductId, requestedProductId);

            try
            {
                await _proposalRepository.CreateExchangeProposal(exchangeProposal);

                return Success.Proposal.RegisterProposal;
            }
            catch (Exception ex)
            {
                return Errors.Proposal.FailedRegisterProposal;
            }
        }
    }
}
