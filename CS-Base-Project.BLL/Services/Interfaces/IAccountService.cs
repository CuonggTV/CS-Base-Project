using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.RequestDTO.Accounts;
using CS_Base_Project.DAL.Data.ResponseDTO.Accounts;

namespace CS_Base_Project.BLL.Services.Interfaces;

public interface IAccountService
{
    Task<AccountEntity> GetAccount();
    Task<GetAccountResponseDTO> GetCurrentAccount();
    Task<ICollection<GetAccountResponseDTO>> GetManyAccounts(int pageNumber, int pageSize);
    Task<GetAccountResponseDTO> GetAccountById(Guid id);
    Task<GetAccountResponseDTO> CreateAccount(CreateAccountRequestDTO requestDto);
    Task<GetAccountResponseDTO> UpdateAccount(Guid id, UpdateAccountRequestDTO requestDto);
    Task<bool> DeleteAccount(Guid id);
}