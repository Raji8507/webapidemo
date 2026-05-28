using webapidemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapidemo.Models;
using webapidemo.DTOs;

namespace EFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET ALL PRODUCTS

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _appDbContext.Products
                .Include(p => p.Category)
                .ToListAsync();

            return Ok(products);
        }

        // GET PRODUCT BY ID

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _appDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(
                    p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Product with ID {id} not found."
                });
            }

            return Ok(product);
        }

        // ADD PRODUCT

        [HttpPost]
        public async Task<IActionResult> AddProduct(
            [FromBody] CreateProductDTO createDto)
        {
            var product = new Product
            {
                ProductName = createDto.ProductName,
                ProductDescription =
                    createDto.ProductDescription,

                ProductPrice =
                    createDto.ProductPrice,

                CatId =
                    createDto.CatId,

                IsAvailable =
                    createDto.IsAvailable
            };

            await _appDbContext.Products
                .AddAsync(product);

            await _appDbContext
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.ProductId },
                product);
        }

        // UPDATE PRODUCT

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProductUpdateDTO updateDto)
        {
            var product =
                await _appDbContext.Products
                .FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message =
                    $"Product with ID {id} not found."
                });
            }

            product.ProductName =
                updateDto.ProductName;

            product.ProductDescription =
                updateDto.ProductDescription;

            product.ProductPrice =
                updateDto.ProductPrice;

            product.CatId =
                updateDto.CatId;

            product.IsAvailable =
                updateDto.IsAvailable;

            _appDbContext.Entry(product)
                .State = EntityState.Modified;

            await _appDbContext
                .SaveChangesAsync();

            return Ok(new
            {
                Message =
                "Product updated successfully.",

                Product = product
            });
        }

        // DELETE PRODUCT

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var product =
                await _appDbContext.Products
                .FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message =
                    $"Product with ID {id} not found."
                });
            }

            _appDbContext.Products
                .Remove(product);

            await _appDbContext
                .SaveChangesAsync();

            return Ok(new
            {
                Message =
                "Product deleted successfully."
            });
        }
    }
}