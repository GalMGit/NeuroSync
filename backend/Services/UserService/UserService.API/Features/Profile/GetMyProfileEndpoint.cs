using Microsoft.AspNetCore.Mvc;
using NeuroSync.MinimalApi.Endpoints;
using UserService.CORE.Interfaces.IServices;

namespace UserService.API.Features.Profile;

public class GetMyProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/user/profile", async (
            [FromHeader(Name = "X-User-Id")] string userId,
            IUserService userService) =>
        {
            try
            {
                if (!Guid.TryParse(userId, out var id) && string.IsNullOrEmpty(userId))
                {
                    return Results.BadRequest("Invalid user ID");
                }
                
                var profile = await userService.GetMyProfileAsync(id);
                return Results.Ok(profile);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}