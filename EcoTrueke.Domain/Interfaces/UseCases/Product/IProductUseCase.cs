using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Product
{
    public interface IProductUseCase
    {
        Task<Result> RegisterProductExecute(string userId, string name, string typeTranscription, IEnumerable<string> category, string status, string? description = null, string? productPicture = null);
    }
}
