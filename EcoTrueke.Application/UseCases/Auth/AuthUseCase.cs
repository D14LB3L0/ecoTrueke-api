using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Domain.Interfaces.UseCases.Auth;
using EcoTrueke.Infrastructure.Security;
using EcoTrueke.Services.API;
using EcoTrueke.Util.Security;
using Newtonsoft.Json;
using static EcoTrueke.Domain.Constants.Errors;

namespace EcoTrueke.Application.UseCases.Auth
{
    public class AuthUseCase : IAuthUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ITokenService _tokenService;
        private readonly IMailerService _mailerService;

        public AuthUseCase(IUserRepository userRepository, IPersonRepository personRepository, INotificationRepository notificationRepository, ITokenService tokenService, IMailerService mailerService)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _notificationRepository = notificationRepository;
            _tokenService = tokenService;
            _mailerService = mailerService;
        }

        public async Task<Result> DeleteAccountExecute(string userId)
        {
            // veify if user exists
            var existUser = await _userRepository.GetUserById(userId);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // if user exists delete user
            try
            {
                await _userRepository.DeleteUser(existUser.Id);
            }
            catch (Exception)
            {
                return Errors.User.FailedToDeleteUser;
            }

            return Success.User.AccountDeleted;

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

            // verify if the password is temporary
            bool isTemporaryPasswordValid = false;

            if (existUser.TemporaryPasswordExpires > DateTime.UtcNow)
            {
                if (password == existUser.TemporaryPassword)
                {
                    isTemporaryPasswordValid = true;
                }
            }

            if (!isTemporaryPasswordValid)
            {
                // convert normal password to hash
                var hashPassword = Encryptor.SHA256Hash(password);

                // verify if the password is different
                if (existUser.Password != hashPassword)
                    return Errors.User.IncorrectPassword;
            }

            // mapping user
            var user = new Domain.Entities.User
            {
                Id = existUser.Id,
                Email = existUser.Email,
                AccountStatus = existUser.AccountStatus,
            };

            // get person 
            var person = await _personRepository.GetPersonById(existUser.PersonId);
            if (person == null)
                return Errors.Person.NotFoundPerson;

            // generate token
            var token = _tokenService.GenerateJWT(user);

            // mapping response
            var loginResponse = new LoginResponse(token, user, person);

            // login success
            return new Result { Code = Success.User.LoggedIn.Code, Data = JsonConvert.SerializeObject(loginResponse), Message = Success.User.LoggedIn.Message };
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
            try
            {
                var person = Domain.Entities.Person.Create(name, paternalSurname, maternalSurname);
                var personResult = await _personRepository.CreatePerson(person);

                try
                {
                    // create user
                    var user = Domain.Entities.User.Create(personResult.Id, email, password);
                    var userResult = await _userRepository.CreateUser(user);

                    // if user is created successfully, notification created
                    var notification = Domain.Entities.Notification.FinishSetup(userResult.Id);
                    try
                    {
                        await _notificationRepository.CreateNotification(notification);

                    }
                    catch (Exception)
                    {
                        return Errors.Notification.FailedToCreateNotification;
                    }
                }
                catch (Exception)
                {
                    return Errors.Person.FailedToCreatePerson;
                }

            }
            catch (Exception)
            {
                return Errors.Person.FailedToCreatePerson;
            }


            return Success.User.Registered;
        }

        public async Task<Result> ResetPasswordExecute(string email)
        {
            // verify if user exists
            var existUser = await _userRepository.GetUserByEmail(email);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // create temporary password
            string temporaryPassword = PasswordGenerator.RandomPassword();

            // assign to user
            existUser.ResetPassword(temporaryPassword);

            try
            {
                await _userRepository.UpdateUser(existUser);
            }
            catch (Exception)
            {
                return Errors.User.FailedToResetPassword;
            }

            // send email to user
            var person = await _personRepository.GetPersonById(existUser.PersonId);

            if (person == null)
                return Errors.Person.NotFoundPerson;

            try
            {
                await _mailerService.SendMailResetPassword(existUser, person, temporaryPassword);
            }
            catch (Exception)
            {
                return Mail.FailedToSendEmail;
            }

            return Success.User.ResetPassword;
        }
    }
}
