using Microsoft.EntityFrameworkCore;
using webapidemo.Data;
using webapidemo.Models;

namespace webapidemo.Services
{
    public class CategoryService
        : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryById(
            int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(
                    c => c.CatId == id);
        }

        public async Task AddCategory(Category category)
        {
            await _context.Categories
                .AddAsync(category);

            await _context
                .SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            var existingCategory =
                await _context.Categories
                .FindAsync(
                    category.CatId);

            if (existingCategory != null)
            {
                existingCategory
                    .CategoryName =
                    category.CategoryName;

                await _context
                    .SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(int id)
        {
            var category =
                await _context.Categories
                .FindAsync(id);

            if (category != null)
            {
                _context.Categories
                    .Remove(category);

                await _context
                    .SaveChangesAsync();
            }
        }
    }
}