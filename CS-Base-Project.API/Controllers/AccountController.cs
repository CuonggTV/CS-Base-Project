using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CS_Base_Project.Controllers;

public class AccountController : BaseController<AccountController>
{
    #region Create Class Reference
    private readonly IAccountService _accountService;
    #endregion
    
    #region Constructors
    public AccountController (ILogger<AccountController> logger, IAccountService accountService) : base(logger)
    {
        _accountService = accountService;
    }

    #endregion

    #region Get Method
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_ACCOUNT_ENDPOINT)]
    public string GetAccount()
    {
        return "Get Account";
    }
    
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_ACCOUNT_BY_ID_ENDPOINT)]
    public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
    {
        var response = await _accountService.GetAccountById(id);
        return Ok(response);
    }

    #endregion
}