

using EcoTrueke.Domain.Entities;
using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IUserRatingRepository
    {
        Task CreateUserRating(UserRating userRating);
    }
}
