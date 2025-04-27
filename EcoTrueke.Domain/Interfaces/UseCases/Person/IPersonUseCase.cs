using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Person
{
    public interface IPersonUseCase
    {
        Task<Result> EditPerson(string userId, string firstName, string paternalSurname, string maternalSurname, string phone,
            string address, string documentNumber, string documentType, string gender);
    }
}
