using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Person;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Person
{
    public class PersonUseCase : IPersonUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;

        public PersonUseCase(IUserRepository userRepository, IPersonRepository personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public async Task<Result> EditPerson(string userId, string firstName, string paternalSurname, string maternalSurname, string phone,
            string address, string documentNumber, string documentType, string gender)
        {
            // veify if user exists
            var existUser = await _userRepository.GetUserById(userId);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // get person
            var person = await _personRepository.GetPersonById(existUser.PersonId);
            if (person == null)
                return Errors.Person.NotFoundPerson;

            // without changes
            if (person.IsSameData(firstName, paternalSurname, maternalSurname, phone, address, documentNumber, documentType, gender))
                return Errors.Person.Unchanged;
            
            // update person
            person.EditPerson(firstName, paternalSurname, maternalSurname, phone,
            address, documentNumber, documentType, gender);


            try
            {
                await _personRepository.UpdatePerson(person);
            }
            catch (Exception)
            {
                return Errors.Person.FailedUpdate;
            }

            // mapping person
            var personMapping = new Domain.Entities.Person
            {
                name = firstName,
                PaternalSurname = paternalSurname,
                MaternalSurname = maternalSurname,
                Phone = phone,
                Address = address,
                DocumentNumber = documentNumber,
                DocumentType = documentType,
                Gender = gender,
            };

            var editPersonResponse = new EditPersonResponse(person);

            // edit person success
            return new Result { Code = Success.Person.UpdatedPerson.Code, Data = JsonConvert.SerializeObject(editPersonResponse), Message = Success.Person.UpdatedPerson.Message };
        }
    }
}
