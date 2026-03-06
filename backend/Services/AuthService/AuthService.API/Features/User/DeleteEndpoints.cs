using System;
using AuthService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NeuroSync.MinimalApi.Endpoints;

namespace AuthService.API.Features.User;

public class DeleteEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth");

        group.MapDelete("delete/{userId:guid}", Delete);
    }

    private async Task<IResult> Delete(
        IUserService userService,
        [FromRoute] Guid userId)
    {
        try
        {
            await userService
                .SoftDeleteAsync(userId);

            return Results.Ok("Удален");
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }

    }
}
