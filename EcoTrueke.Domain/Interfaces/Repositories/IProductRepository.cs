using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task RegisterProduct(Product product);

        Task <Product?> GetProductById(string productId); 
    }
}
