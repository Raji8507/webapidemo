using Microsoft.AspNetCore.Mvc;
using webapidemo.Models;
using webapidemo.Services;

namespace webapidemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController
        : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories =
                await _service
                .GetAllCategory();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(
            int id)
        {
            var category =
                await _service
                .GetCategoryById(id);

            if (category == null)
            {
                return NotFound(
                    new
                    {
                        Message =
                        "Category Not Found"
                    });
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _service
                .AddCategory(category);

            return Ok(new
            {
                Message =
                "Category Added Successfully"
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            await _service
                .UpdateCategory(category);

            return Ok(new
            {
                Message =
                "Category Updated Successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _service
                .DeleteCategory(id);

            return Ok(new
            {
                Message =
                "Category Deleted Successfully"
            });
        }
    }
}