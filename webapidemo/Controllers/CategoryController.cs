using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapidemo.Data;
using webapidemo.DTOs;
using webapidemo.Models;

namespace demowebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext
            _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET ALL

        [HttpGet]
        public async Task<IActionResult>
            GetCategories()
        {
            var categories =
                await _appDbContext
                .Categories
                .Select(c => new CategoryDTO
                {
                    CatId = c.CatId,

                    CategoryName =
                        c.CategoryName
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET BY ID

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetCategoryById(int id)
        {
            var category =
                await _appDbContext
                .Categories
                .FirstOrDefaultAsync(
                    c => c.CatId == id);

            if (category == null)
            {
                return NotFound(
                    new
                    {
                        Message =
                        "Category Not Found"
                    });
            }

            return Ok(new CategoryDTO
            {
                CatId = category.CatId,

                CategoryName =
                    category.CategoryName
            });
        }

        // ADD

        [HttpPost]
        public async Task<IActionResult>
            AddCategory(
            [FromBody]
            CreateCategoryDTO dto)
        {
            var category =
                new Category
                {
                    CategoryName =
                        dto.CategoryName
                };

            await _appDbContext
                .Categories
                .AddAsync(category);

            await _appDbContext
                .SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategoryById),

                new
                {
                    id = category.CatId
                },

                category);
        }

        // UPDATE

        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateCategory(
            int id,

            [FromBody]
            CategoryUpdateDTO dto)
        {
            var category =
                await _appDbContext
                .Categories
                .FindAsync(id);

            if (category == null)
            {
                return NotFound(
                    new
                    {
                        Message =
                        "Category Not Found"
                    });
            }

            category.CategoryName =
                dto.CategoryName;

            _appDbContext
                .Entry(category)
                .State =
                EntityState.Modified;

            await _appDbContext
                .SaveChangesAsync();

            return Ok(new
            {
                Message =
                "Category Updated",

                Category =
                category
            });
        }

        // DELETE

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteCategory(int id)
        {
            var category =
                await _appDbContext
                .Categories
                .FindAsync(id);

            if (category == null)
            {
                return NotFound(
                    new
                    {
                        Message =
                        "Category Not Found"
                    });
            }

            _appDbContext
                .Categories
                .Remove(category);

            await _appDbContext
                .SaveChangesAsync();

            return Ok(new
            {
                Message =
                "Category Deleted"
            });
        }
    }
}