using System.Net;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Domain.Interfaces.UseCases.Person;
using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Person
{
    public class PersonUseCase : IPersonUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IFileService _fileService;

        public PersonUseCase(IUserRepository userRepository, IPersonRepository personRepository, IFileService fileService)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _fileService = fileService;
        }

        public async Task<Result> EditPersonExecute(string userId, string firstName, string paternalSurname, string maternalSurname, string phone,
            string documentNumber, string documentType, string? address = null, string? gender = null, IFormFile? profilePicture = null, string? profilePictureRemove = null)
        {
            // veify if user exists
            var existUser = await _userRepository.GetUserById(userId);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // get person
            var person = await _personRepository.GetPersonById(existUser.PersonId);
            if (person == null)
                return Errors.Person.NotFoundPerson;

            // identify if profile picture exist
            string? profilePicturePath = person.ProfilePicture;

            if (profilePicture != null)
                profilePicturePath = await _fileService.SaveProfilePictureAsync(person.Id, profilePicture);

            if (profilePictureRemove != null)
            {
                await _fileService.DeletPictureAsync(person.ProfilePicture);
                profilePicturePath = "";
            }

            // without changes
            if (person.IsSameData(firstName, paternalSurname, maternalSurname, phone, address, documentNumber, documentType, gender, profilePicturePath))
                return Errors.Person.Unchanged;

            // update person
            person.EditPerson(firstName, paternalSurname, maternalSurname, phone,
            address, documentNumber, documentType, gender, profilePicturePath);

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
                Name = firstName,
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

        public async Task<Result> GetPersonExecute(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                try
                {
                    var person = await _personRepository.GetPersonById(user.PersonId);

                    var getPersonResponse = new GetPersonResponse(person);
                    return new Result { Code = Success.Person.GetPerson.Code, Data = JsonConvert.SerializeObject(getPersonResponse), Message = Success.Person.GetPerson.Message };
                }
                catch (Exception)
                {
                    return Errors.Person.NotFoundPerson;
                }
            }
            catch (Exception)
            {
                return Errors.User.NotFoundUser;
            }
        }
    }
}
