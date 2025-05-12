using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.Services;
using EcoTrueke.Domain.Interfaces.UseCases.Product;
using EcoTrueke.Services.API;
using Microsoft.AspNetCore.Http;

namespace EcoTrueke.Application.UseCases.Product
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public ProductUseCase(IProductRepository productRepository, IFileService fileService, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _fileService = fileService;
            _userRepository = userRepository;
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
