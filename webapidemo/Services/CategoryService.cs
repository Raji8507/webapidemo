using webapidemo.Models;
using webapidemo.Services;

namespace demowebapi.Services
{
    public class CategoryService : ICategoryService
    {
        private static List<Category> _categories = new()
        {
            new Category
            {
                CatId = 1,
                CategoryName = "Electronics"
            },

            new Category
            {
                CatId = 2,
                CategoryName = "Accessories"
            },

            new Category
            {
                CatId = 3,
                CategoryName = "Fashion"
            }
        };

        public IEnumerable<Category> GetAllCategory()
        {
            return _categories;
        }

        public Category? GetCategoryById(int id)
        {
            return _categories
                .FirstOrDefault(
                    c => c.CatId == id);
        }

        public void AddCategory(Category category)
        {
            category.CatId =
                _categories.Max(
                    c => c.CatId) + 1;

            _categories.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            var existing =
                _categories.FirstOrDefault(
                    c => c.CatId ==
                    category.CatId);

            if (existing != null)
            {
                existing.CategoryName =
                    category.CategoryName;
            }
        }

        public void DeleteCategory(int id)
        {
            var category =
                _categories.FirstOrDefault(
                    c => c.CatId == id);

            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}