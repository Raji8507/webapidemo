using Microsoft.EntityFrameworkCore;
using webapidemo.Data;
using webapidemo.Models;

namespace webapidemo.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products
                .Include(
                    p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products
                .Include(
                    p => p.Category)
                .FirstOrDefaultAsync(
                    p => p.ProductId == id);
        }

        public async Task AddProduct(Product product)
        {
            await _context.Products
                .AddAsync(product);

            await _context
                .SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var existingProduct =
                await _context.Products
                .FindAsync(
                    product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductName =
                    product.ProductName;

                existingProduct.ProductDescription =
                    product.ProductDescription;

                existingProduct.ProductPrice =
                    product.ProductPrice;

                existingProduct.CatId =
                    product.CatId;

                existingProduct.IsAvailable =
                    product.IsAvailable;

                await _context
                    .SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(int id)
        {
            var product =
                await _context.Products
                .FindAsync(id);

            if (product != null)
            {
                _context.Products
                    .Remove(product);

                await _context
                    .SaveChangesAsync();
            }
        }
    }
}