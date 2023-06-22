using CatalogAPI.Models;
using DataAccess.Abstract;
using DataAccess.Repositories;
using Domain.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ProductServices : IProductService
    {
        private readonly IEntityBaseRepository<Product> _productRepository;
        private readonly ILogger<ProductServices> _logger;

        public ProductServices(IEntityBaseRepository<Product> productRepository, ILogger<ProductServices> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product to database.");
                throw new Exception("Error adding product to database.", ex);
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product in database.");
                throw new Exception("Error updating product in database.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product from database.");
                throw new Exception("Error deleting product from database.", ex);
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                return await _productRepository.GetAllAsync(x => x.Category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting list of products from database.");
                throw new Exception("Error getting list of products from database.", ex);
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                return await _productRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product from database.");
                throw new Exception("Error getting product from database.", ex);
            }
        }
    }
}
