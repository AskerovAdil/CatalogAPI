using AutoMapper;
using CatalogAPI.Models;
using Domain.Abstract;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            var catalogItem = await _productService.GetByIdAsync(id);
            return catalogItem == null ? NotFound() : catalogItem;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddAsync([FromBody] ProductDto productDto)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(productDto);
                await _productService.AddAsync(newProduct);
                _logger.LogInformation("Added new product with id {Id}", newProduct.Id);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new product");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ProductDto productDto)
        {
            try
            {
                var updateProduct = _mapper.Map<Product>(productDto);
                updateProduct.Id = id;
                await _productService.UpdateAsync(updateProduct);
                _logger.LogInformation("Updated product with id {Id}", id);
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                _logger.LogInformation("Deleted product with id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
