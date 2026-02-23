using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shared.WebApi;

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
                throw new UnauthorizedAccessException();
            
            return Guid.Parse(userIdClaims);
        }
    }
    
    protected string Username
    {
        get
        {
            var usernameClaims = User?.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(usernameClaims))
                throw new UnauthorizedAccessException();

            return usernameClaims;
        }
    }
    
    protected string UserEmail
    {
        get
        {
            var userEmailClaims = User?.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmailClaims))
                throw new UnauthorizedAccessException();

            return userEmailClaims;
        }
    }
    
    protected bool IsInRole(string role)
    {
        return User.IsInRole(role) 
               || UserRole.Equals(role, StringComparison.OrdinalIgnoreCase);
    }

    protected string UserRole
    {
        get
        {
            var userRole = User?.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(userRole))
                throw new UnauthorizedAccessException();

            return userRole;
        }
    }
    
}