using E_commers.Application.Exaptions;
using E_commers.Domain.Interface;
using E_commers.Domain.Models;
using E_commers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commers.Infrastructure.Repo
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGeneric<TEntity> where TEntity : class
    {
        public async Task<int> AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
           return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entity is null)
                return 0; 
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

       
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();    
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(id);
            return result;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

       

    }
}
