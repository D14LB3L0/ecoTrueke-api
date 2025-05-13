using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Domain.Interfaces.UseCases.Product;
using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Product
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductQuery _productQuery;
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public ProductUseCase(IProductRepository productRepository, IProductQuery productQuery, IFileService fileService, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _productQuery = productQuery;
            _fileService = fileService;
            _userRepository = userRepository;
        }

        public async Task<Result> GetPaginatedProductExecute(int page, int amountPage, string loggedUserId)
        {
            var (products, totalPages) = await _productQuery.GetPaginatedProducts(page, amountPage, loggedUserId);

            var paginationResponse = new GetPaginatedProductResponse(products, totalPages);

            return new Result { Code = Success.Product.GetPaginatedProducts.Code, Message = Success.Product.GetPaginatedProducts.Message, Data = JsonConvert.SerializeObject(paginationResponse) };
        }

        public async Task<Result> GetProductExecute(string productId)
        {
            // get product 
            var existProduct = await _productRepository.GetProductById(productId);

            // validate if product exists
            if (existProduct == null)
                return Errors.Product.NotFoundProduct;

            var productResponse = new ProductResponse(existProduct);

            return new Result { Code = Success.Product.ProductFound.Code, Data = JsonConvert.SerializeObject(productResponse), Message = Success.Product.ProductFound.Message };

        }

        public async Task<Result> RegisterProductExecute(string userId, string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, IFormFile? productPicture = null)
        {
            try
            {
                // get user
                var existUser = await _userRepository.GetUserById(userId);

                if (existUser == null)
                    return Errors.User.NotFoundUser;

                string? productPicturePath = null;

                // upload picture
                if (productPicture != null)
                {
                    productPicturePath = await _fileService.SaveProductPictureAsync(existUser.PersonId, productPicture);
                }

                var product = Domain.Entities.Product.RegisterProduct(userId, name, typeTranscription, category, condition, quantity, description, productPicturePath);

                await _productRepository.RegisterProduct(product);

                return Success.Product.RegisteredProduct;
            }
            catch (Exception)
            {
                return Errors.Product.FailedRegisterProduct;
            }
        }
    }
}
