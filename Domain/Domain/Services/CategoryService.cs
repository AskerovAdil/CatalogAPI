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
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<Product> productRepository, ILogger<ProductService> logger)
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
                _logger.LogError(ex, "Error adding product");
                throw;
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
                _logger.LogError(ex, "Error updating product");
                throw;
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
                _logger.LogError(ex, "Error deleting product");
                throw;
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}
