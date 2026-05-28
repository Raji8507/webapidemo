using Microsoft.AspNetCore.Mvc;
using webapidemo.Models;
using webapidemo.Services;

namespace webapidemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products =
                await _service
                .GetAllProduct();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(
            int id)
        {
            var product =
                await _service
                .GetProductById(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message =
                    "Product Not Found"
                });
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _service
                .AddProduct(product);

            return Ok(new
            {
                Message =
                "Product Added Successfully"
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            await _service
                .UpdateProduct(product);

            return Ok(new
            {
                Message =
                "Product Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service
                .DeleteProduct(id);

            return Ok(new
            {
                Message =
                "Product Deleted Successfully"
            });
        }
    }
}