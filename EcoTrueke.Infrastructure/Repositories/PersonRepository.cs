using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public PersonRepository(IDatabaseRepository databaseRepository)
        {
            this._databaseRepository = databaseRepository;
        }

        public async Task<Person> CreatePerson(Person person)
        {
            // map data
            var mongoPerson = PersonMapper.ToMongo(person);

            // insert person
            var resultMongoPerson = await _databaseRepository.InsertOneAsync(mongoPerson);

            // map data
            var domainPerson = PersonMapper.ToDomain(resultMongoPerson);

            return domainPerson;
        }

        public async Task DeletePerson(string personId)
        {
            await _databaseRepository.UpdateOneAsync<MongoModels.Person>(
                p => p.Id == personId && p.IsDeleted != true,
                p => p.IsDeleted = true);
        }

        public async Task<Person?> GetPersonById(string personId)
        {
            var person = await _databaseRepository.FindOneAsync<MongoModels.Person>(
                 p => p.Id == personId && p.IsDeleted != true);

            return PersonMapper.ToDomain(person);
        }
    }
}
