using webapidemo.Models;

namespace webapidemo.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProduct();

        Task<Product?> GetProductById(int id);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);
    }
}