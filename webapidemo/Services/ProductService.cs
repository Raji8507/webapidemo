using Microsoft.EntityFrameworkCore;
using webapidemo.Data;
using webapidemo.DTOs;
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

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _context.Products
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductPrice = p.ProductPrice,
                    CatId = p.CatId,
                    IsAvailable = p.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<ProductDTO?> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
                return null;
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                CatId = product.CatId,
                IsAvailable = product.IsAvailable
            };
        }

        public async Task<ProductDTO> AddProduct(CreateProductDTO dto)
        {
            var product =
                new Product
                {
                    ProductName = dto.ProductName,
                    ProductDescription = dto.ProductDescription,
                    ProductPrice = dto.ProductPrice,
                    CatId = dto.CatId,
                    IsAvailable = dto.IsAvailable
                };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                CatId = product.CatId,
                IsAvailable = product.IsAvailable
            };
        }

        public async Task<ProductDTO?> UpdateProduct(int id, ProductUpdateDTO dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;
            product.ProductName = dto.ProductName;
            product.ProductDescription = dto.ProductDescription;
            product.ProductPrice = dto.ProductPrice;
            product.CatId = dto.CatId;
            product.IsAvailable = dto.IsAvailable;
            await _context.SaveChangesAsync();
            return await GetProductById(id);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}