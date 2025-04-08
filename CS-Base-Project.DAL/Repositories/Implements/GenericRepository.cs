using CS_Base_Project.DAL.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CS_Base_Project.DAL.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private DbContext _dbContext;
    private DbSet<T> _dbSet;

    #region MyRegion
    public Task<T> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    #endregion
    

    public Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}