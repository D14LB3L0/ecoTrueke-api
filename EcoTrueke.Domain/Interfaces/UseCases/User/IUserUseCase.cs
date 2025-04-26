using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.User
{
    public interface IUserUseCase
    {
        Task<Result> ChangePasswordExecute(string userId, string password);
    }
}
