using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> CreatePerson(Person person);
        Task DeletePerson(string personId);
    }
}
