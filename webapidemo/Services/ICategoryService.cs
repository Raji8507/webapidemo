using webapidemo.DTOs;

namespace webapidemo.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO?> GetCategoryById(int id);
        Task<CategoryDTO> AddCategory(CreateCategoryDTO dto);
        Task<CategoryDTO?> UpdateCategory(int id, CategoryUpdateDTO dto);
        Task DeleteCategory(int id);
    }
}