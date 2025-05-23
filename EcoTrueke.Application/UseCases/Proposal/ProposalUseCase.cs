using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Proposal
{
    public class ProposalUseCase : IProposalUseCase
    {

        private readonly IProposalRepository _proposalRepository;
        private readonly IProposalQuery _proposalQuery;
        private readonly INotificationRepository _notificationRepository;

        public ProposalUseCase(IProposalRepository proposalRepository, INotificationRepository notificationRepository, IProposalQuery proposalQuery)
        {
            _proposalRepository = proposalRepository;
            _notificationRepository = notificationRepository;
            _proposalQuery = proposalQuery;
        }

        public Task<Result> GetProposalAcceptedExecute(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GetProposalsExecute(int page, int amountPage, string loggedUserId)
        {
            var response = await _proposalQuery.GetProposals(page, amountPage, loggedUserId);

            // login success
            return new Result { Code = Success.User.LoggedIn.Code, Data = JsonConvert.SerializeObject(response), Message = Success.User.LoggedIn.Message };
        }

        public async Task<Result> GetProposalsRequestedExecute(string userId)
        {
            var getProposal = await _proposalRepository.GetProposalsByUserId(userId);

            var proposals = getProposal.Select(p => new Domain.Entities.Proposal
            {
                Id = p.Id,
                ProposerId = p.ProposerId,
                OwnerId = p.OwnerId,
                ProposalType = p.ProposalType,
                OfferedProductId = p.OfferedProductId,
                RequestedProductId = p.RequestedProductId,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            }).ToList();

            var proposalResponse = new GetProposalsRequestedResponse(proposals);

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
                try
                {
                    var notification = Domain.Entities.Notification.ExchangeRequest(ownerId);
                    await _notificationRepository.CreateNotification(notification);

                }
                catch (Exception)
                {
                    return Errors.Notification.FailedToCreateNotification;
                }

                return Success.Proposal.RegisterProposal;
            }
            catch (Exception ex)
            {
                return Errors.Proposal.FailedRegisterProposal;
            }
        }
    }
}
