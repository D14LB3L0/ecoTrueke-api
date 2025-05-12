using EcoTrueke.API.Requests.Product;
using EcoTrueke.Domain.Interfaces.UseCases.Product;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> RegisterProduct([FromForm] RegisterProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _productUseCase.RegisterProductExecute(LoggedUserId,request.Name, request.TypeTranscription,request.Category, request.Condition, request.Quantity, request.Description, request.ProductPicture);

                return StatusCode(result.Code, new { message = result.Message });

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
