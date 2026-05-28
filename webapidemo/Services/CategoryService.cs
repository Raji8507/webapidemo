using Microsoft.EntityFrameworkCore;
using webapidemo.Data;
using webapidemo.DTOs;
using webapidemo.Models;

namespace webapidemo.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            return await _context.Categories.AsNoTracking()
                .Select(c => new CategoryDTO
                {
                    CatId =
                        c.CatId,

                    CategoryName =
                        c.CategoryName
                })
                .ToListAsync();
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            var category =await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CatId == id);
            if (category == null)
                return null;
            return new CategoryDTO
            {
                CatId = category.CatId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryDTO> AddCategory(CreateCategoryDTO dto)
        {
            var category = new Category
                {
                    CategoryName = dto.CategoryName
                };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return new CategoryDTO
            {
                CatId = category.CatId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryDTO?> UpdateCategory(int id, CategoryUpdateDTO dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return null;
            category.CategoryName = dto.CategoryName;
            await _context.SaveChangesAsync();
            return await GetCategoryById(id);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}