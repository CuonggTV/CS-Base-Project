using AutoMapper;
using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.Exceptions;
using CS_Base_Project.DAL.Data.Repositories.Interfaces;
using CS_Base_Project.DAL.Data.RequestDTO.Accounts;
using CS_Base_Project.DAL.Data.ResponseDTO.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CS_Base_Project.BLL.Services.Implements;

public class AccountService(
    IUnitOfWork<AppDbContext> unitOfWork, 
    ILogger<AccountEntity> logger,
    IMapper mapper)
    : BaseService<AccountEntity>(unitOfWork, logger), IAccountService
{
    private readonly PasswordHasher<object> _passwordHasher = new();
    
    public Task<AccountEntity> GetAccount()
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<GetAccountResponseDTO>> GetManyAccounts(int pageNumber, int pageSize)
    {
        var accounts = await _unitOfWork.GetRepository<AccountEntity>().GetPagingListAsync(
            pageIndex: pageNumber,
            pageSize: pageSize);

        return accounts.Items.Select(mapper.Map<AccountEntity, GetAccountResponseDTO>).ToList();
    }

    public async Task<GetAccountResponseDTO> GetAccountById(Guid id)
    {
        try
        {
            var account = await _unitOfWork.GetRepository<AccountEntity>().FirstOrDefaultAsync(
                predicate: a => a.Id == id,
                include: a => a.Include(a => a.RoleEntity));
            return mapper.Map<AccountEntity, GetAccountResponseDTO>(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting claim: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<GetAccountResponseDTO> CreateAccount(CreateAccountRequestDTO requestDto)
    {
        var account = mapper.Map<CreateAccountRequestDTO, AccountEntity>(requestDto);
        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            // Check if Role exists
            var findRoleResult = await _unitOfWork.GetRepository<RoleEntity>().FirstOrDefaultAsync(
                predicate: r => r.Name == requestDto.Role);
            if (findRoleResult == null) throw new NotFoundException("Role not found!");
            account.RoleEntity = findRoleResult;
            
            // Hash Password
            var hashedPassword = _passwordHasher.HashPassword(null, requestDto.Password);
            account.Password = hashedPassword;
            
            // Add Account
            await _unitOfWork.GetRepository<AccountEntity>().InsertAsync(account);
            
        });
        return mapper.Map<AccountEntity, GetAccountResponseDTO>(account);;
    }

    public async Task<GetAccountResponseDTO> UpdateAccount(Guid id, UpdateAccountRequestDTO requestDto)
    {
        // Check if Account exists
        var account = await _unitOfWork.GetRepository<AccountEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id,
            include: a => a.Include(a => a.RoleEntity));
        if (account == null) throw new NotFoundException("Account not found!");
        
        // Update Account
        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            // Map UpdateAccountRequestDTO to AccountEntity
            mapper.Map(requestDto, account);
            
            // Update
            _unitOfWork.GetRepository<AccountEntity>().UpdateAsync(account);
            
        });
        return mapper.Map<AccountEntity, GetAccountResponseDTO>(account);
    }

    public async Task<bool> DeleteAccount(Guid id)
    {
        var account = await _unitOfWork.GetRepository<AccountEntity>().FirstOrDefaultAsync(
            predicate: a => a.Id == id,
            include: a => a.Include(a => a.RoleEntity));
        if (account == null) throw new NotFoundException("Account not found!");
        
        // Delete Account
        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            _unitOfWork.GetRepository<AccountEntity>().Delete(account);
            
        });
        return true;
    }
}