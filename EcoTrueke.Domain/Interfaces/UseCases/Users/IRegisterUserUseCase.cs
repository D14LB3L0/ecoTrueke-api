using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Users
{
    public interface IRegisterUserUseCase
    {
        Task<Result> Execute(string name, string paternalSurname, string maternalSurname, string email, string password, string confirmPassword);
    };
}
