using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{

    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : class
    {
        private ApplicationDbContext _context;
        public EntityBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding entity to database.", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity in database.", ex);
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
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting entity from database.", ex);
            }
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            try
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting list of entities from database.", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("The id cannot be negative.", nameof(id));
            }

            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    throw new InvalidOperationException("The entity with the specified id was not found.");
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting entity from database.", ex);
            }
        }
    }    
}
