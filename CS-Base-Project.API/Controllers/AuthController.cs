using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.Constants;
using CS_Base_Project.DAL.Data.Metadatas;
using CS_Base_Project.DAL.Data.RequestDto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CS_Base_Project.Controllers;

public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService
) : BaseController<AuthController>(logger)
{
    [HttpPost(APIEndpointsConstant.AuthEndpoints.LOGIN_ENDPOINT)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO requestDto)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Login successful",
            data: await authService.HandleLogin(requestDto)
        ));
    }
   
}