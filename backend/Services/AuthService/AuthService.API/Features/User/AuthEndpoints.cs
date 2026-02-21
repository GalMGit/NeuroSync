using AuthService.CORE.Interfaces.IServices;
using NeuroSync.MinimalApi.Endpoints;
using Shared.Contracts.DTOs.Auth.Requests;

namespace AuthService.API.Features.User;

public class AuthEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth");
        
        group.MapPost("register", Register);
        group.MapPost("login", Login);
    }
    
    private async Task<IResult> Register(
        IUserService userService,
        RegisterRequest request)
    {
        var email = await userService
            .CreateAsync(request);
        
        return Results.Ok(email);
    }
    
    private async Task<IResult> Login(
        IUserService userService,
        LoginRequest request)
    {
        var token = await userService
            .LoginAsync(request);
        
        return Results.Ok(token);
    }
}