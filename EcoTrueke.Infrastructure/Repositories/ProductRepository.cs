using EcoTrueke.Domain.Entities;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Infrastructure.Mappers;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public ProductRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task RegisterProduct(Product product)
        {
            // map data
            var mongoProduct = ProductMapper.ToMongo(product);

            // insert product
            await _databaseRepository.InsertOneAsync(mongoProduct);
        }
    }
}
