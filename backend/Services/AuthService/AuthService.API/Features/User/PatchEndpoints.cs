using System;
using System.Security.Claims;
using AuthService.API.Extensions;
using AuthService.CORE.Interfaces.IServices;
using NeuroSync.MinimalApi.Endpoints;

namespace AuthService.API.Features.User;

public class PatchEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth");

        group.MapPatch("delete", SoftDelete)
            .RequireAuthorization();
    }

    private async Task<IResult> SoftDelete(
        ClaimsPrincipal claims,
        IUserService userService)
    {
        try
        {
            var userId = claims.GetUserId();

            await userService
                .SoftDeleteAsync(userId);

            return Results.Ok("Удален");
        }
        catch (Exception e)
        {
            return Results.Problem(
                title: "Ошибка удаления аккаунта",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }

    }
}
