using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;
using UserService.CORE.Interfaces.IServices;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserGetController(
    IUserService userService
    ) : BaseController
{
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        try
        {
            var profile = await userService
                .GetUserProfileAsync(UserId);

            return Ok(profile);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserProfile(Guid userId)
    {
        try
        {
            var profile = await userService
                .GetUserProfileAsync(userId);

            if (profile == null)
            {
                return NotFound(new { 
                    message = "Пользователь не найден",
                    errorCode = "USER_NOT_FOUND" 
                });
            }

            return Ok(profile);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            if (e.Message.Contains("удален"))
            {
                return NotFound(new { 
                    message = e.Message,
                    errorCode = "USER_DELETED"
                });
            }
        
            if (e.Message.Contains("не найден"))
            {
                return NotFound(new { 
                    message = e.Message,
                    errorCode = "USER_NOT_FOUND"
                });
            }

            return BadRequest(new { 
                message = e.Message,
                errorCode = "BAD_REQUEST"
            });
        }
    }
}