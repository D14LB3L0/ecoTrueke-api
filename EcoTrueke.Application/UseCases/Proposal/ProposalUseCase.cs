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
        private readonly IProductRepository _productRepository;
        private readonly IMailerService _mailerService;

        public ProposalUseCase(IProposalRepository proposalRepository, INotificationRepository notificationRepository, IProposalQuery proposalQuery, IUserRepository userRepository, IPersonRepository personRepository, IMailerService mailer, IProductRepository productRepository)
        {
            _proposalRepository = proposalRepository;
            _notificationRepository = notificationRepository;
            _proposalQuery = proposalQuery;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _mailerService = mailer;
            _productRepository = productRepository;
        }

        public async Task<Result> GetProposalsExecute(int page, int amountPage, string status, string loggedUserId)
        {
            var (proposals, totalPages) = await _proposalQuery.GetProposals(page, amountPage, status, loggedUserId);

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
                    await _proposalRepository.UpdateStatusProposal(proposalId, action);

                }
                catch
                {
                    return Errors.Proposal.FailedRespondProposal;
                }

                if (action == Types.ProposalStatus.Accepted)
                {
                    try
                    {
                        var proposal = await _proposalRepository.GetProposalById(proposalId);

                        try
                        {
                            var ownerUser = await _userRepository.GetUserById(proposal.OwnerId);
                            var proposerUser = await _userRepository.GetUserById(proposal.ProposerId);

                            try
                            {
                                var ownerPerson = await _personRepository.GetPersonById(ownerUser.PersonId);
                                var proposerPerson = await _personRepository.GetPersonById(proposerUser.PersonId);

                                try
                                {
                                    var ownerProduct = await _productRepository.GetProductById(proposal.RequestedProductId);
                                    var proposerProduct = await _productRepository.GetProductById(proposal.OfferedProductId);

                                    // update status
                                    if (ownerProduct != null && proposerProduct != null)
                                    {
                                        // owner
                                        ownerProduct.Status = Types.ProductStatus.Pending;
                                        ownerProduct.UpdatedAt = DateTime.UtcNow;

                                        // proposer
                                        proposerProduct.Status = Types.ProductStatus.Pending;
                                        proposerProduct.UpdatedAt = DateTime.UtcNow;
                                    }

                                    try
                                    {
                                        await _productRepository.UpdateProduct(ownerProduct);
                                        await _productRepository.UpdateProduct(proposerProduct);
                                    }
                                    catch
                                    {
                                        return Errors.Product.FailedUpdate;
                                    }
                                }
                                catch
                                {
                                    return Errors.Product.NotFoundProduct;
                                }

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

        public async Task<Result> ConfirmOrCancelProposalExecute(string proposalId, string productAction, string proposalAction)
        {
            try
            {
                try
                {
                    try
                    {
                        // get proposal
                        var proposal = await _proposalRepository.GetProposalById(proposalId);

                        // get producs
                        var offeredProduct = await _productRepository.GetProductById(proposal.OfferedProductId);
                        var requestedProduct = await _productRepository.GetProductById(proposal.RequestedProductId);

                        try
                        {
                            await _proposalRepository.UpdateStatusProposal(proposalId, proposalAction);
                        }
                        catch (Exception)
                        {
                            return Errors.Proposal.FailedRespondProposal;
                        }

                        if (proposalAction == Types.ProposalStatus.Cancelled)
                        {
                            try
                            {
                                // change status active again
                                offeredProduct.Status = Types.ProductStatus.Active;
                                requestedProduct.Status = Types.ProductStatus.Active;

                                await _productRepository.UpdateProduct(offeredProduct);
                                await _productRepository.UpdateProduct(requestedProduct);

                                return Success.Proposal.CancelProposal;
                            }
                            catch (Exception)
                            {
                                return Errors.Product.FailedUpdate;
                            }

                        }
                        else
                        {
                            offeredProduct.Status = Types.ProductStatus.Traded;
                            requestedProduct.Status = Types.ProductStatus.Traded;

                            await _productRepository.UpdateProduct(offeredProduct);
                            await _productRepository.UpdateProduct(requestedProduct);

                            return Success.Proposal.ConfirmProposal;
                        }

                    }
                    catch (Exception)
                    {
                        return Errors.Proposal.FailedGetProposal;
                    }
                }
                catch (Exception)
                {
                    return Errors.Proposal.FailedRespondProposal;
                }
            }
            catch (Exception)
            {
                return Errors.Proposal.FailedRespondProposal;
            }
        }
    }
}
