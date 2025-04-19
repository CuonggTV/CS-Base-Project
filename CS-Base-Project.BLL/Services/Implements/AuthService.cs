using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.Repositories.Interfaces;
using CS_Base_Project.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CS_Base_Project.BLL.Services.Implements;

public class AuthService
    (IUnitOfWork<AppDbContext> unitOfWork, ILogger<AuthService> logger, IConfiguration configuration)
    : BaseService<AuthService>(unitOfWork, logger), IAuthService
{
    public async Task<string> HandleLogin(LoginRequestDTO loginRequest)
    {
        var account =await unitOfWork.GetRepository<AccountEntity>()
            .FirstOrDefaultAsync(
                predicate: a => a.Email == loginRequest.Email && a.Password == loginRequest.Password
                );
        var tokenHandler = new JwtSecurityTokenHandler();
        var key =  Encoding.ASCII.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}