using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.User
{
    public interface IUserUseCase
    {
        Task<Result> ChangePassword(string userId, string password);
    }
}
