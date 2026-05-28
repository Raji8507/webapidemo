using webapidemo.Models;

namespace webapidemo.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProduct();

        Product GetProductById(int id);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int id);
    }
}