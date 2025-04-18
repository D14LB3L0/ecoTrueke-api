using EcoTrueke.Domain.Entities;
using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.Services
{
    public interface IMailerService
    {
        Task<Result> SendMailResetPassword(User user, Person person, string newPassword);
    }
}
