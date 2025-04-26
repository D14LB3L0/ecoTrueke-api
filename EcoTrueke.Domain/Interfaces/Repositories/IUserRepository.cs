
using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(string userId);
        Task DeleteUser(string userId);
        Task ChangePassword(string userId, string newPasswordHash);
        Task UpdateUser(User user);
    }
}
