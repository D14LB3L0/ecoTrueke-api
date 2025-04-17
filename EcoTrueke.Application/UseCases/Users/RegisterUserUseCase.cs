using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Users;
using EcoTrueke.Services.API;

namespace EcoTrueke.Application.UseCases.Users
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        public RegisterUserUseCase(IUserRepository userRepository, IPersonRepository personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public async Task<Result> Execute(string name, string paternalSurname, string maternalSurname, string email, string password, string confirmPassword)
        {
            // verify if the password match
            if (password != confirmPassword)
                return Errors.User.PasswordsDoNotMatch;

            // verify if user exixsts
            var existsUser = await _userRepository.GetUserByEmail(email);
            if (existsUser != null)
                return Errors.User.AlreadyExists;

            // create person
            var person = Person.Create(name, $"{paternalSurname} {maternalSurname}");
            var personResult = await _personRepository.CreatePerson(person);

            // create user
            var user = User.Create(personResult.Id, email, password);
            var userResult = await _userRepository.CreateUser(user);

            return Success.User.Registered;
        }
    }
}
