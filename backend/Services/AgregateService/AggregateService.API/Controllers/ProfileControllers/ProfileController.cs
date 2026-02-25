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

        await Task.WhenAll(profileTask, postsTask);

        var profileResult = profileTask.Result;
        var postsResult = postsTask.Result;

        if (!profileResult.Success && profileResult.ErrorCode == "UNAUTHORIZED")
        {
            return Unauthorized(new
            {
                message = "Необходима авторизация",
                errorCode = "UNAUTHORIZED"
            });
        }

        if (!profileResult.Success)
        {
            return StatusCode(503, new
            {
                message = "Сервис пользователей временно недоступен",
                errorCode = profileResult.ErrorCode
            });
        }

        var profileWithPosts = new ProfileWithPosts
        {
            Profile = profileResult.Data!,
            Posts = postsResult.Success ? postsResult.Data ?? [] : []
        };

        if (!postsResult.Success)
        {
            return Ok(new
            {
                message = "Профиль загружен, но сервис постов временно недоступен",
                data = profileWithPosts,
                warnings = new[] { "Posts service unavailable" }
            });
        }

        return Ok(profileWithPosts);
    }
}