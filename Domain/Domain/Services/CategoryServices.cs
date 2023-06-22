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
    public class CategoryServices : ICategoryService
    {
        private readonly IEntityBaseRepository<Category> _categoryRepository;
        private readonly ILogger<CategoryServices> _logger;

        public CategoryServices(IEntityBaseRepository<Category> categoryRepository, ILogger<CategoryServices> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task AddAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            try
            {
                await _categoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding category to database.");
                throw new Exception("Error adding category to database.", ex);
            }
        }

        public async Task UpdateAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            try
            {
                await _categoryRepository.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category in database.");
                throw new Exception("Error updating category in database.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The id cannot be negative.", nameof(id));
            }

            try
            {
                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category from database.");
                throw new Exception("Error deleting category from database.", ex);
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                return await _categoryRepository.GetAllAsync(x => x.Products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting list of categories from database.");
                throw new Exception("Error getting list of categories from database.", ex);
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The id cannot be negative.", nameof(id));
            }

            try
            {
                return await _categoryRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting category from database.");
                throw new Exception("Error getting category from database.", ex);
            }
        }
    }
}
