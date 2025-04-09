using CS_Base_Project.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CS_Base_Project.Controllers;

public class AccountController : BaseController<AccountController>
{
    #region Constructors
    public AccountController (ILogger<AccountController> logger) : base(logger)
    {
    }

    #endregion

    #region Get Method
    [HttpGet(APIEndpointsConstant.AccountEndpoints.GET_ACCOUNT_ENDPOINT)]
    public string GetAccount()
    {
        return "Get Account";
    }

    #endregion
}