using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Proposal
{
    public class ProposalUseCase : IProposalUseCase
    {

        private readonly IProposalRepository _proposalRepository;
        private readonly IProposalQuery _proposalQuery;
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMailerService _mailerService;

        public ProposalUseCase(IProposalRepository proposalRepository, INotificationRepository notificationRepository, IProposalQuery proposalQuery, IUserRepository userRepository, IPersonRepository personRepository, IMailerService mailer)
        {
            _proposalRepository = proposalRepository;
            _notificationRepository = notificationRepository;
            _proposalQuery = proposalQuery;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _mailerService = mailer;
        }

        public Task<Result> GetProposalAcceptedExecute(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GetProposalsExecute(int page, int amountPage, string loggedUserId)
        {
            var (proposals, totalPages) = await _proposalQuery.GetProposals(page, amountPage, loggedUserId);

            var paginationResponse = new GetPaginatedProposalResponse(proposals, totalPages);

            // login success
            return new Result { Code = Success.User.LoggedIn.Code, Data = JsonConvert.SerializeObject(paginationResponse), Message = Success.User.LoggedIn.Message };
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
            catch (Exception)
            {
                return Errors.Proposal.FailedRegisterProposal;
            }
        }

        public async Task<Result> RejectOrAcceptProposalExecute(string proposalId, string action)
        {
            try
            {
                try
                {
                    await _proposalRepository.RejectOrAcceptProposal(proposalId, action);

                }
                catch
                {
                    return Errors.Proposal.FailedRespondProposal;
                }

                if (action == Types.ProposalStatus.Accepted)
                {
                    try
                    {
                        var proposal = await _proposalRepository.GetProposalId(proposalId);

                        try
                        {
                            var ownerUser = await _userRepository.GetUserById(proposal.OwnerId);
                            var proposerUser = await _userRepository.GetUserById(proposal.ProposerId);

                            try
                            {
                                var ownerPerson = await _personRepository.GetPersonById(ownerUser.PersonId);
                                var proposerPerson = await _personRepository.GetPersonById(proposerUser.PersonId);

                                // create notifications
                                var notificationOwner = Domain.Entities.Notification.ProposalAccepted(ownerUser.Id);
                                var notificationProposer = Domain.Entities.Notification.ProposerUserRequestAccepted(proposerUser.Id);

                                try
                                {
                                    await _notificationRepository.CreateNotification(notificationOwner);
                                    await _notificationRepository.CreateNotification(notificationProposer);
                                }
                                catch (Exception)
                                {
                                    return Errors.Notification.FailedToCreateNotification;
                                }
                                try
                                {
                                    // create mails
                                    await _mailerService.SendMailExchangeAcceptedByOwner(ownerUser, ownerPerson, proposerPerson);
                                    await _mailerService.SendMailExchangeAcceptedToProposer(proposerUser, proposerPerson, ownerPerson);
                                }
                                catch (Exception)
                                {
                                    return Errors.Mail.FailedToSendEmail;
                                }
                            }
                            catch (Exception)
                            {
                                return Errors.Person.NotFoundPerson;
                            }
                        }
                        catch
                        {
                            return Errors.User.NotFoundUser;
                        }
                    }
                    catch (Exception)
                    {
                        return Errors.Proposal.FailedGetProposal;
                    }
                }
                else
                {
                    return Success.Proposal.RejectProposal;
                }
                    return Success.Proposal.AcceptProposal;
            }
            catch (Exception)
            {
                return Errors.Proposal.FailedRespondProposal;
            }
        }
    }
}
