using ApiGateway.services;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Auth.Requests;
using Shared.Contracts.DTOs.Auth.Responses;

namespace ApiGateway.Controllers.Auth;
[ApiController]
[Route("api")]
public class AuthPostController(
    AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(
        LoginRequest request)
    {
        var token = await authService
            .LoginAsync(request);

        return token;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        var email = await authService
            .RegisterAsync(request);

        return email;
    }
}