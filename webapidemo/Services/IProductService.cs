using webapidemo.DTOs;

namespace webapidemo.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();

        Task<ProductDTO?> GetProductById(int id);

        Task<ProductDTO> AddProduct(CreateProductDTO dto);

        Task<ProductDTO?> UpdateProduct(int id, ProductUpdateDTO dto);

        Task DeleteProduct(int id);
    }
}