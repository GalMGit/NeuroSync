using AggregateService.API.DTOs;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Community.Responses;
using Shared.Contracts.DTOs.Post.Responses;
using Shared.WebApi;

namespace AggregateService.API.Controllers.CommunityControllers;

[Route("aggregate/communities")]
[ApiController]
public class CommunityController(
    ICommunityServiceClient communityServiceClient,
    IPostServiceClient postServiceClient
) : BaseController
{
    [HttpGet("{communityId:guid}")]
    public async Task<IActionResult> GetCommunityWithPostsAsync(Guid communityId)
    {
        try
        {
            var communityTask = communityServiceClient
                .GetCommunityAsync(communityId);

            var postsTask = postServiceClient
                .GetPostsByCommunityAsync(communityId);

            await Task.WhenAll(communityTask, postsTask);

            var community = communityTask.Result;
            var posts = postsTask.Result;

            if (community == null)
            {
                return Problem(
                    title: "Сообщество не найдено",
                    detail: $"Сообщество с идентификатором {communityId} не найдено",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            var communityWithPosts = new CommunityWithPosts
            {
                Community = community,
                Posts = posts ?? []
            };

            return Ok(communityWithPosts);
        }
        catch (ServiceException ex)
        {
            return Problem(
                title: ex.Title,
                detail: ex.Message,
                statusCode: (int)ex.StatusCode
            );
        }
        catch (Exception)
        {
            return Problem(
                title: "Внутренняя ошибка сервера",
                detail: "Произошла непредвиденная ошибка",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}