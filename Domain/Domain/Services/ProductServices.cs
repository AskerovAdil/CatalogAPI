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

        public ProductServices(IEntityBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
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
                throw;
            }
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync(x=>x.Category);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}
