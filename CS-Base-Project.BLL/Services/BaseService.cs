using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CS_Base_Project.BLL.Services;

public abstract class BaseService<TEntity> where TEntity : class
{
    protected IUnitOfWork<AppDbContext> _unitOfWork { get; }
    protected ILogger<TEntity> _logger { get; }
    
    protected BaseService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<TEntity> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

}