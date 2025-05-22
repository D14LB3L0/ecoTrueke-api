using EcoTrueke.Application.UseCases.Auth;
using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Proposal
{
    public class ProposalUseCase : IProposalUseCase
    {

        private readonly IProposalRepository _proposalRepository;

        public ProposalUseCase(IProposalRepository proposalRepository)
        {
            _proposalRepository = proposalRepository;
        }

        public async Task<Result> GetProposalsExecute(string userId)
        {
           var getProposal = await _proposalRepository.GetProposalsByUserId(userId);

            var proposals = getProposal.Select(p => new Domain.Entities.Proposal
            {
                Id = p.Id,
                ProposerId= p.ProposerId,
                OwnerId= p.OwnerId,
                ProposalType= p.ProposalType,
                OfferedProductId= p.OfferedProductId,
                RequestedProductId= p.RequestedProductId,
                Status= p.Status,
                CreatedAt= p.CreatedAt
            }).ToList();

            var proposalResponse = new GetProposalsResponse(proposals);

            return new Result { Code = Success.Proposal.GetProposal.Code, Data = JsonConvert.SerializeObject(proposalResponse), Message = Success.Proposal.GetProposal.Message };

        }

        public async Task<Result> RegisterExchangeProposalExecute(string proposerId, string ownerId, string proposalType, string offeredProductId, string requestedProductId)
        {
            // create exchange proposal
            var exchangeProposal = Domain.Entities.Proposal.Create(proposerId, ownerId, proposalType, offeredProductId, requestedProductId);

            try
            {
                await _proposalRepository.CreateExchangeProposal(exchangeProposal);

                // send notification
                var notification = Domain.Entities.Notification.ExchangeRequest(ownerId);

                return Success.Proposal.RegisterProposal;
            }
            catch (Exception ex)
            {
                return Errors.Proposal.FailedRegisterProposal;
            }
        }
    }
}
