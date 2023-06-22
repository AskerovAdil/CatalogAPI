using AutoMapper;
using CatalogAPI.Models;
using Domain.Abstract;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryServices> _logger;
        
        public CategoryController(ICategoryService categoryService, ILogger<CategoryServices> logger)
        {
            _logger = logger;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetByIdAsync(int id)
        {
            var catalogItem = await _categoryService.GetByIdAsync(id);
            return catalogItem == null ? NotFound() : catalogItem;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddAsync([FromForm] string name)
        {
            try
            {
                var newCategory = new Category() { Name = name };
                await _categoryService.AddAsync(newCategory);
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new category");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] string name)
        {
            try
            {
                var updateCategory = new Category() { Id = id, Name = name };

                await _categoryService.UpdateAsync(updateCategory);
                return Ok(updateCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
