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

        public async Task DeleteProduct(string productId)
        {
            await _databaseRepository.UpdateOneAsync<MongoModels.Product>(
                p => p.Id == productId && p.IsDeleted != true,
                p =>
                {
                    p.UpdatedAt = DateTime.UtcNow;
                    p.IsDeleted = true;
                });
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

        public async Task UpdateProduct(Product product)
        {
            // map to mongo entity
            var mongoProduct = ProductMapper.ToMongo(product);

            // replace the document completly
            await _databaseRepository.ReplaceOneAsync(
                p => p.Id == product.Id && p.IsDeleted != true,
                mongoProduct);
        }
    }
}
