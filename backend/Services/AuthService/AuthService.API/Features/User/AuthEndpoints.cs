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
        try
        {
            var email  = await userService
                .CreateAsync(request);

            return Results.Ok(email);
        }
        catch (Exception e)
        {
            return Results.Problem(
                title: "Ошибка регистрации",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }

    }

    private async Task<IResult> Login(
        IUserService userService,
        LoginRequest request)
    {
        if(string.IsNullOrEmpty(request.Email)
            || string.IsNullOrEmpty(request.Password))
        {
            return Results.Problem(
                title: "Запрос неверный",
                detail: "Проверьте заполненность всех полей",
                statusCode: StatusCodes.Status400BadRequest
            );
        }
        try
        {
            var token = await userService
                .LoginAsync(request);

            return Results.Ok(token);
        }
        catch (Exception e)
        {
            return Results.Problem(
                title: "Ошибка входа",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    }
}