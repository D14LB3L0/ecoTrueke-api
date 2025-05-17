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

        public async Task<Result> DeleteProductExecute(string productId)
        {
            try
            {
                await _productRepository.DeleteProduct(productId);

                return new Result { Code = Success.Product.ProductDeleted.Code, Message = Success.Product.ProductDeleted.Message };
            }
            catch (Exception)
            {
                return Errors.Product.FailedToDeleteProduct;
            }
        }

        public async Task<Result> EditProductExecute(string userId, string productId, string name, string typeTranscription, IEnumerable<string> category, string condition, string quantity, string? description = null, IFormFile? productPicture = null, string? productPictureRemove = null)
        {
            // get user
            var existUser = await _userRepository.GetUserById(userId);

            if (existUser == null)
                return Errors.User.NotFoundUser;

            // get product
            var product = await _productRepository.GetProductById(productId);

            // validate if product exists
            if (product == null)
                return Errors.Product.NotFoundProduct;

            // identify if product picture exist
            string? productPicturePath = product.ProductPicture;

            if (productPicture != null)
                productPicturePath = await _fileService.SaveProductPictureAsync(existUser.PersonId, productPicture);

            if (productPictureRemove != null)
            {
                await _fileService.DeletPictureAsync(product.ProductPicture);

                productPicturePath = "";
            }

            // without changes
            if (product.IsSameData(name, typeTranscription, category, condition, quantity, description, productPicturePath))
                return Errors.Product.Unchanged;

            // update product
            product.EditProduct(name, typeTranscription, category, condition, quantity, description, productPicturePath);

            try
            {
                await _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return Errors.Product.FailedUpdate;
            }

            return new Result { Code = Success.Product.UpdatedProduct.Code, Message = Success.Product.UpdatedProduct.Message };

        }

        public async Task<Result> GetPaginatedProductExecute(int page, int amountPage, string loggedUserId, bool? myProducts = false)
        {
            var (products, totalPages) = await _productQuery.GetPaginatedProducts(page, amountPage, loggedUserId, myProducts);

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
