
using CS_Base_Project.DAL.Data.RequestDto.Auth;

namespace CS_Base_Project.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<string> HandleLogin(LoginRequestDTO loginRequestDto);
}