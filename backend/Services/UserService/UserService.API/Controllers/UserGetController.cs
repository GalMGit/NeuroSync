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
                .GetMyProfileAsync(UserId);

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
}