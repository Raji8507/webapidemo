using webapidemo.Models;

namespace demowebapi.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategory();

        Category? GetCategoryById(int id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(int id);
    }
}