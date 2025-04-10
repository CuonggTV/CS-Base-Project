namespace CS_Base_Project.DAL.Data.Repositories.Interfaces;

public interface IGenericRepositoryFactory
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}