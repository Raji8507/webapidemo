using webapidemo.Models;

namespace webapidemo.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategory();

        Task<Category?> GetCategoryById(int id);

        Task AddCategory(Category category);

        Task UpdateCategory(Category category);

        Task DeleteCategory(int id);
    }
}