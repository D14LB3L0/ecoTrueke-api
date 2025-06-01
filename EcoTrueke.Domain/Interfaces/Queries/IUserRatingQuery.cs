using EcoTrueke.Domain.DTOs;

namespace EcoTrueke.Domain.Interfaces.Queries
{
    public interface IUserRatingQuery
    {
        Task<GetUserRatingResponse> GetUserRatingByUserId(string userId); 
    }
}
