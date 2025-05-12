using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Product;
using EcoTrueke.Services.API;

namespace EcoTrueke.Application.UseCases.Product
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public ProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Result> RegisterProductExecute(string userId, string name, string typeTranscription, IEnumerable<string> category, string status, string? description = null, string? productPicture = null)
        {
            throw new NotImplementedException();
        }
    }
}
