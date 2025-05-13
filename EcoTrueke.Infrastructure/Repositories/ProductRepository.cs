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

        public async Task<Product?> GetProductById(string productId)
        {
            var mongoProduct = await _databaseRepository.FindOneAsync<MongoModels.Product>(
                p => p.Id == productId && p.IsDeleted != true
                );

            // if the user doesn't exists
            if (mongoProduct == null)
                return null;

            return ProductMapper.ToDomain(mongoProduct);
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
