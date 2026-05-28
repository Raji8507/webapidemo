using Microsoft.AspNetCore.Mvc;
using webapidemo.DTOs;
using webapidemo.Services;

namespace webapidemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _service.GetAllCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(
            [FromBody]
            CreateCategoryDTO dto)
        {
            var category = await _service.AddCategory(dto);
            return CreatedAtAction(nameof(GetCategoryById),
                new
                {
                    id = category.CatId
                },
                category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,
            [FromBody]
            CategoryUpdateDTO dto)
        {
            var category = await _service.UpdateCategory(id, dto);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _service.GetCategoryById(id);
            if (category == null)
                return NotFound();
            await _service.DeleteCategory(id);
            return Ok(new
            {
                Message = "Category Deleted Successfully"
            });
        }
    }
}