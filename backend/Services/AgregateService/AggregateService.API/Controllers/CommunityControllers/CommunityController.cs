using AggregateService.API.DTOs;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        var postTask = postServiceClient
            .GetPostsByCommunityAsync(communityId);
        var communityTask = communityServiceClient
            .GetCommunityAsync(communityId);

        await Task.WhenAll(postTask, communityTask);

        var postResult = postTask.Result;
        var communityResult = communityTask.Result;

        if (!communityResult.Success && communityResult.ErrorCode == "NOT_FOUND")
        {
            return NotFound(new { message = "Сообщество не найдено" });
        }

        if (!communityResult.Success)
        {
            return StatusCode(503, new
            {
                message = "Сервис сообществ временно недоступен",
                errorCode = communityResult.ErrorCode
            });
        }

        var communityWithPosts = new CommunityWithPosts
        {
            Community = communityResult.Data,
            Posts = postResult.Success
                ? postResult.Data ?? []
                : []
        };

        if (!postResult.Success)
        {
            return Ok(new
            {
                message = "Сервис постов временно недоступен",
                data = communityWithPosts
            });
        }

        return Ok(communityWithPosts);
    }
}

