using EcoTrueke.API.Requests.Product;
using EcoTrueke.API.Responses;
using EcoTrueke.API.Responses.Product;
using EcoTrueke.Domain.Interfaces.UseCases.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {

        private readonly IProductUseCase _productUseCase;

        public ProductController(IProductUseCase productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedProducts([FromQuery] GetPaginatedProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _productUseCase.GetPaginatedProductExecute(request.Page, request.AmountPage, LoggedUserId);

                var paginatedProductsResponse = JsonConvert.DeserializeObject<GetPaginatedProductResponse>(result.Data);

                var apiResponse = new ApiResponse<GetPaginatedProductResponse>(paginatedProductsResponse!, result.Message);

                return StatusCode(result.Code, apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
            {
            try
            {
                var result = await _productUseCase.GetProductExecute(productId);

                var productResponse = JsonConvert.DeserializeObject<GetProductResponse>(result.Data);

                var apiResponse = new ApiResponse<GetProductResponse>(productResponse, result.Message);

                return StatusCode(result.Code, apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct([FromForm] RegisterProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _productUseCase.RegisterProductExecute(LoggedUserId, request.Name, request.TypeTranscription, request.Category, request.Condition, request.Quantity, request.Description, request.ProductPicture);

                return StatusCode(result.Code, new { message = result.Message });

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
