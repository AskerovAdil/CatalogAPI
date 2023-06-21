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

        public CategoryServices(IEntityBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync(x=>x.Products);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
    }
}
