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
            Posts = postsResult.Success ? postsResult.Data
            ?? []
            : []
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

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserByIdProfile(Guid userId)
    {
        var profileTask = userClient.GetUserByIdAsync(userId);
        var postsTask = postClient.GetUserPostsAsync();

        await Task.WhenAll(profileTask, postsTask);

        var profileResult = profileTask.Result;
        var postsResult = postsTask.Result;

        if (!profileResult.Success)
        {
            if (profileResult.IsUserDeleted)
            {
                return NotFound(new
                {
                    message = profileResult.ErrorMessage ?? "Пользователь был удален",
                    errorCode = "USER_DELETED",
                    isDeleted = true
                });
            }

            if (profileResult.IsNotFound)
            {
                return NotFound(new
                {
                    message = profileResult.ErrorMessage ?? "Пользователь не найден",
                    errorCode = "USER_NOT_FOUND",
                    isDeleted = false
                });
            }

            if (profileResult.IsUnauthorized)
            {
                return Unauthorized(new
                {
                    message = "Необходима авторизация",
                    errorCode = "UNAUTHORIZED"
                });
            }

            if (profileResult.IsServiceUnavailable)
            {
                return StatusCode(503, new
                {
                    message = profileResult.ErrorMessage ?? "Сервис пользователей временно недоступен",
                    errorCode = "USER_SERVICE_UNAVAILABLE"
                });
            }

            return StatusCode(profileResult.StatusCode, new
            {
                message = profileResult.ErrorMessage ?? "Ошибка при получении данных пользователя",
                errorCode = profileResult.ErrorCode ?? "USER_SERVICE_ERROR"
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