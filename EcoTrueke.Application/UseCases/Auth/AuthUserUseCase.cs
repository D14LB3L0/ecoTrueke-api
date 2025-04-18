using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Domain.Interfaces.UseCases.Auth;
using EcoTrueke.Infrastructure.Security;
using EcoTrueke.Services.API;
using EcoTrueke.Util.Security;
using Newtonsoft.Json;
using static EcoTrueke.Domain.Constants.Types;

namespace EcoTrueke.Application.UseCases.Auth
{
    public class AuthUserUseCase : IAuthUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ITokenService _tokenService;
        private readonly IMailerService _mailerService;
        public AuthUserUseCase(IUserRepository userRepository, IPersonRepository personRepository, ITokenService tokenService, IMailerService mailerService)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _tokenService = tokenService;
            _mailerService = mailerService;
        }

        public async Task<Result> LoginExecute(string email, string password)
        {
            // veify if user exists
            var existUser = await _userRepository.GetUserByEmail(email);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // verify account status
            if (existUser.AccountStatus == Types.AccountStatus.Suspended)
                return Errors.User.AccountStatutsSuspended;

            // convert normal password to hash
            var hashPassword = Encryptor.SHA256Hash(password);

            // verify if the password is different
            if (existUser.Password != hashPassword)
                return Errors.User.IncorrectPassword;

            // mapping user
            var user = new User
            {
                Id = existUser.Id,
                Email = existUser.Email,
                AccountStatus = existUser.AccountStatus,
            };

            // generate token
            var token = _tokenService.GenerateJWT(user);

            // mapping response
            var loginResponse = new LoginResponse(token, user.Id, user.Email, user.AccountStatus);

            // login success
            return new Result { Code = Success.User.LoggedIn.Code, Data = (JsonConvert.SerializeObject(loginResponse)), Message = Success.User.LoggedIn.Message };
        }

        public async Task<Result> RegisterExecute(string name, string paternalSurname, string maternalSurname, string email, string password, string confirmPassword)
        {
            // verify if the password match
            if (password != confirmPassword)
                return Errors.User.PasswordsDoNotMatch;

            // verify if user exists
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

        public async Task<Result> ResetPasswordExecute(string email)
        {
            // verify if user exists
            var existUser = await _userRepository.GetUserByEmail(email);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // create new password and hashed
            string newPassword = PasswordGenerator.RandomPassword();
            string hashedPassword = Encryptor.SHA256Hash(newPassword);

            // updated password
            try
            {
                await _userRepository.ResetUserPassword(existUser.Id, hashedPassword);
            }
            catch (Exception ex)
            {
                return Errors.User.FailedToResetPassword;
            }

            // send email to user
            var person = await _personRepository.GetPersonById(existUser.PersonId);

            if (person == null)
                return Errors.Person.NotFoundPerson;

            try
            {
                await _mailerService.SendMailResetPassword(existUser, person, newPassword);
            }
            catch (Exception ex)
            {
                return Errors.Mail.FailedToSendEmail;
            }

            return Success.User.ResetPassword;
        }
    }
}
