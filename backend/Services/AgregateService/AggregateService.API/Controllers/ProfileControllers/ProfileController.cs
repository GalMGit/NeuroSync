using AggregateService.API.DTOs;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace AggregateService.API.Controllers.ProfileControllers;

[ApiController]
[Route("aggregate/profile")]
public class ProfileController(
    IUserServiceClient userClient,
    IPostServiceClient postClient) : BaseController
{
    [HttpGet("full")]
    public async Task<IActionResult> GetUserFullProfile()
    {
        var profile = await userClient
            .GetProfileAsync();
        
        var posts = await postClient
            .GetUserPostsAsync();

        var profileWithPosts = new ProfileWithPosts
        {
            Posts = posts,
            Profile = profile
        };
        
        return Ok(profileWithPosts);
    }
}