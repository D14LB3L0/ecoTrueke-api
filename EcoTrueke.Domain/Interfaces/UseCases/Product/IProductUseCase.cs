using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;

namespace EcoTrueke.Domain.Interfaces.UseCases.Product
{
    public interface IProductUseCase
    {
        Task<Result> RegisterProductExecute(string userId, string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, IFormFile? productPicture = null);
        Task<Result> GetPaginatedProductExecute(int page, int amountPage, string loggedUserId, bool? myProducts = false, string? searchTerm = null);
        Task<Result> GetProductExecute(string productId);
        Task<Result> EditProductExecute(string userId, string productId, string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, IFormFile? productPicture = null, string? productPictureRemove = null);
        Task<Result> DeleteProductExecute(string productId);
    }
}
