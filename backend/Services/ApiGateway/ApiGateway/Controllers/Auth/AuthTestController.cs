using ApiGateway.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers.Auth;
[ApiController]
[Route("api")]
public class AuthTestController(
    AuthService authService
    ) : ControllerBase
{
    [HttpGet("test")]
    [Authorize]
    public async Task<ActionResult<string>> TestAsync()
    {
        var test = await authService
            .GetTestAsync();
        
        return test;
    }
}