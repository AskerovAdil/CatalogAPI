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

        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
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
        public async Task<ActionResult<Product>> AddAsync([FromBody] ProductDto productDto )
        {
            try
            {
                var newProduct = _mapper.Map<Product>(productDto);
                await _productService.AddAsync(newProduct);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ProductDto productDto)
        {
            try
            {
                var updateProduct= _mapper.Map<Product>(productDto);
                updateProduct.Id = id;
                await _productService.UpdateAsync(updateProduct);
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
