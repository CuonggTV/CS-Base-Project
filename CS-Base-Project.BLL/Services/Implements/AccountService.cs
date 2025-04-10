using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CS_Base_Project.BLL.Services.Implements;

public class AccountService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<AccountEntity> logger)
    : BaseService<AccountEntity>(unitOfWork, logger), IAccountService
{
    public Task<AccountEntity> GetAccount()
    {
        throw new NotImplementedException();
    }

    public async Task<AccountEntity> GetAccountById(Guid id)
    {
        try
        {
            return await _unitOfWork.GetRepository<AccountEntity>().FirstOrDefaultAsync(
                predicate: a => a.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting claim: {Message}", ex.Message);
            throw;
        }
    }

    public Task<AccountEntity> CreateAccount(AccountEntity account)
    {
        throw new NotImplementedException();
    }

    public Task<AccountEntity> UpdateAccount(AccountEntity account)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAccount(int id)
    {
        throw new NotImplementedException();
    }
}