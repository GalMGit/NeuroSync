using AuthService.CORE.Interfaces.IServices;
using NeuroSync.MinimalApi.Endpoints;
using Shared.Contracts.DTOs.Auth.Requests;

namespace AuthService.API.Features.User;

public class RestoreEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth");

        group.MapPatch("restore", Restore);
    }

    private async Task<IResult> Restore(
        IUserService userService,
        LoginRequest request)
    {
        try
        {
            await userService
                .RestoreAccountAsync(request);

            return Results.Ok("Аккаунт восстановлен");
        }
        catch (Exception e)
        {
             return Results.Problem(
                title: "Ошибка восстановления аккаунта",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }

    }
}