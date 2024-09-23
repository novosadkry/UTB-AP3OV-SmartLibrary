namespace SmartLibrary.Domain.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<PagedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
