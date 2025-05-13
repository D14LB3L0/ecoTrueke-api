using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Queries
{
    public interface IProductQuery
    {
        Task<(List<Product> Products, int totalPages)> GetPaginatedProducts(int page, int amountPage, string loggedUserId);
    }
}
