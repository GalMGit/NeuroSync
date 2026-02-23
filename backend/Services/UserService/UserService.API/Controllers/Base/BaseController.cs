using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers.Base;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            var userIdClaims = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaims))
                throw new UnauthorizedAccessException("UserId not found");
            
            return Guid.Parse(userIdClaims);
        }
    }
}