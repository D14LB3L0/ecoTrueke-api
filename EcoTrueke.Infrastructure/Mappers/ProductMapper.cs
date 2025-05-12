namespace EcoTrueke.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static MongoModels.Product ToMongo(Domain.Entities.Product product)
        {
            return new MongoModels.Product
            {
                Id = product.Id,
                UserId = product.UserId,
                ProductPicture = product.ProductPicture,
                Name = product.Name,
                Description = product.Description,
                TypeTranscription = product.TypeTranscription,
                Category = product.Category,
                Quantity = product.Quantity,
                Condition = product.Condition,
                Status = product.Status,
                UpdatedAt = product.UpdatedAt,
                CreatedAt = product.CreatedAt,
                IsDeleted = product.IsDeleted,
            };
        }

        public static Domain.Entities.Product ToDomain(MongoModels.Product mongoProduct)
        {
            return new Domain.Entities.Product
            {
                Id = mongoProduct.Id,
                UserId = mongoProduct.UserId,
                ProductPicture = mongoProduct.ProductPicture,
                Name = mongoProduct.Name,
                Description = mongoProduct.Description,
                TypeTranscription = mongoProduct.TypeTranscription,
                Category = mongoProduct.Category,
                Quantity= mongoProduct.Quantity,    
                Condition = mongoProduct.Condition,
                Status = mongoProduct.Status,
                UpdatedAt = mongoProduct.UpdatedAt,
                CreatedAt = mongoProduct.CreatedAt,
                IsDeleted = mongoProduct.IsDeleted,
            };
        }
    }
}
