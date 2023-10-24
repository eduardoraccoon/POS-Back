using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Product.Request;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts()
        {
            var response = await _productApplication.ListProducts();
            return Ok(response);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productApplication.GetAllProducts();
            return Ok(response);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _productApplication.ProductById(productId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("{productId:int}")]
        public async Task<IActionResult> EditProduct(int productId, [FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.EditProduct(productId, requestDto);
            return Ok(response);
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var response = await _productApplication.RemoveProduct(productId);
            return Ok(response);
        }
    }
}
