using AggregateService.API.DTOs;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace AggregateService.API.Controllers.ProfileControllers;

[ApiController]
[Route("aggregate/profile")]
public class ProfileController(
    IUserServiceClient userClient,
    IPostServiceClient postClient
) : BaseController
{
    [HttpGet("full")]
    public async Task<IActionResult> GetUserFullProfile()
    {
        var profileTask = userClient.GetProfileAsync();
        var postsTask = postClient.GetUserPostsAsync();

        await Task.WhenAll(postsTask, profileTask);

        var profileResult = await profileTask;
        var postsResult = await postsTask;

        var profileWithPosts = new ProfileWithPosts
        {
            Profile = profileResult.Success ? profileResult.Data : null,
            Posts = postsResult.Success ? postsResult.Data ?? [] : []
        };
        
        if (!profileResult.Success && profileResult.ErrorCode == "UNAUTHORIZED")
        {
            return Unauthorized(new { message = "Необходима авторизация" });
        }
        
        if (!profileResult.Success && !postsResult.Success)
        {
            return StatusCode(503, new { 
                message = "Сервисы временно недоступны",
                details = profileWithPosts 
            });
        }

        return Ok(profileWithPosts);
    }
}