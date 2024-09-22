using Microsoft.EntityFrameworkCore;
using SmartLibrary.Core.Data;
using SmartLibrary.Core.Interfaces;

namespace SmartLibrary.Core.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<PagedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var dbSet = _dbContext.Set<TEntity>();
            var totalCount = await dbSet.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (float)pageSize);

            var items = await dbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TEntity>
            {
                Items = items,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPages = totalPages
            };
        }
    }
}
