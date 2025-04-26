using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.User;
using EcoTrueke.Services.API;
using EcoTrueke.Util.Security;

namespace EcoTrueke.Application.UseCases.User
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> ChangePassword(string userId, string password)
        {
            // verify if user exists
            var existUser = await _userRepository.GetUserById(userId);
            if (existUser == null)
                return Errors.User.NotFoundUser;

            // encrypt
            var encryptPassword = Encryptor.SHA256Hash(password);

            // update password
            try
            {
                await _userRepository.ChangePassword(userId, encryptPassword);
            }
            catch (Exception)
            {
                return Errors.User.FailedToResetPassword;
            }

            return Success.User.ChangePassword;
        }
    }
}
