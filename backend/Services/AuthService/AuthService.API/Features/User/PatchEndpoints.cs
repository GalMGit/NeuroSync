using System;
using System.Security.Claims;
using AuthService.API.Extensions;
using AuthService.CORE.Interfaces.IServices;
using NeuroSync.MinimalApi.Endpoints;
using Shared.Contracts.DTOs.Auth.Requests;

namespace AuthService.API.Features.User;

public class PatchEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth");

        group.MapPatch("delete", SoftDelete)
            .RequireAuthorization();

        group.MapPatch("restore", Restore);
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
