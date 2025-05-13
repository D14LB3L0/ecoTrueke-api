using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;

namespace EcoTrueke.Domain.Interfaces.UseCases.Product
{
    public interface IProductUseCase
    {
        Task<Result> RegisterProductExecute(string userId, string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, IFormFile? productPicture = null);
        Task<Result> GetPaginatedProductExecute(int page, int amountPage, string loggedUserId);
        Task<Result> GetProductExecute(string productId);
    }
}
