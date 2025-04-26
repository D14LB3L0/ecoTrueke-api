using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Auth
{
    public interface IAuthUseCase
    {
        Task<Result> RegisterExecute(string name, string paternalSurname, string maternalSurname, string email, string password, string confirmPassword);

        Task<Result> LoginExecute(string email, string password);

        Task<Result> ResetPasswordExecute(string email);

        Task<Result> DeleteAccountExecute(string userId);
    };
}
