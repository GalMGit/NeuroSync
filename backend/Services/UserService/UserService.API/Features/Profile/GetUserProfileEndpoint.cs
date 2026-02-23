using NeuroSync.MinimalApi.Endpoints;
using UserService.CORE.Interfaces.IServices;

namespace UserService.API.Features.Profile;

public class GetUserProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/user/{userId:guid}", async (
            IUserService userService,
            Guid userId) =>
        {
            try
            {
                var profile = await userService
                    .GetUserProfileAsync(userId);
                
                return Results.Ok(profile);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}