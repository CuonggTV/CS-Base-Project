using CS_Base_Project.DAL.Data.Entities;

namespace CS_Base_Project.BLL.Services.Interfaces;

public interface IAccountService
{
    Task<AccountEntity> GetAccount();
    Task<AccountEntity> GetAccountById(Guid id);
    Task<AccountEntity> CreateAccount(AccountEntity account);
    Task<AccountEntity> UpdateAccount(AccountEntity account);
    Task<bool> DeleteAccount(int id);
}