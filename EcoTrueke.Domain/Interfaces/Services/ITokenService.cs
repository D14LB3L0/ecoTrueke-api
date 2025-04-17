using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJWT(User user);
    }
}
