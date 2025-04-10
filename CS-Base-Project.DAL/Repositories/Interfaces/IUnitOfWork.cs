using Microsoft.EntityFrameworkCore;

namespace CS_Base_Project.DAL.Data.Repositories.Interfaces;

public interface IUnitOfWork : IGenericRepositoryFactory, IDisposable
{
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);
    
    Task ExecuteInTransactionAsync(Func<Task> action);
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    TContext Context { get; }
}