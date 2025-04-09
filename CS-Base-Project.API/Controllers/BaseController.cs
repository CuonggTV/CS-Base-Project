using CS_Base_Project.Constants;
using Microsoft.AspNetCore.Mvc;

namespace CS_Base_Project.Controllers;

[Route(APIEndpointsConstant.API_ENDPOINT)]
[ApiController]
public class BaseController<T>
    (ILogger<T> logger)
    : ControllerBase where T : BaseController<T>
{
    
}