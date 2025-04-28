using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;

namespace EcoTrueke.Domain.Interfaces.UseCases.Person
{
    public interface IPersonUseCase
    {
        Task<Result> EditPerson(string userId, string firstName, string paternalSurname, string maternalSurname, string phone,
            string documentNumber, string documentType, string? address = null, string? gender = null, IFormFile? profilePicture = null, string? profilePictureRemove = null);
    }
}
