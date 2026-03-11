using AggregateService.API.DTOs;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Post.Responses;
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
        try
        {
            var profile = await userClient
                .GetProfileAsync();

            IEnumerable<PostResponse> posts = [];

            try
            {
                posts = await postClient
                    .GetUserPostsAsync();
            }
            catch (ServiceException ex)
            {

            }

            var profileWithPosts = new ProfileWithPosts
            {
                Profile = profile,
                Posts = posts ?? []
            };

            return Ok(profileWithPosts);
        }
        catch (ServiceException ex)
        {

            return Problem(
                title: ex.Title,
                detail: ex.Message,
                statusCode: (int)ex.StatusCode
            );
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Внутренняя ошибка сервера",
                detail: "Произошла непредвиденная ошибка",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserByIdProfile(Guid userId)
    {
        try
        {
            var profile = await userClient
                .GetUserByIdAsync(userId);

            IEnumerable<PostResponse> posts = [];

            try
            {
                posts = await postClient
                    .GetUserPostsAsync();
            }
            catch (ServiceException ex)
            {
            }

            var profileWithPosts = new ProfileWithPosts
            {
                Profile = profile,
                Posts = posts ?? []
            };

            return Ok(profileWithPosts);
        }
        catch (ServiceException ex)
        {
            return Problem(
                title: ex.Title,
                detail: ex.Message,
                statusCode: (int)ex.StatusCode
            );
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Внутренняя ошибка сервера",
                detail: "Произошла непредвиденная ошибка",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}