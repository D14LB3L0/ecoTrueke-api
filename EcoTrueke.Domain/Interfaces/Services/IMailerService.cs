using EcoTrueke.Domain.Entities;
using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.Services
{
    public interface IMailerService
    {
        Task<Result> SendMailResetPassword(User user, Person person, string newPassword);

        Task<Result> SendMailExchangeAcceptedByOwner(User ownerUser, Person ownerPerson, Person proposalPerson);
        Task<Result> SendMailExchangeAcceptedToProposer(User proposalUser, Person proposalPerson, Person ownerPerson);
    }
}
