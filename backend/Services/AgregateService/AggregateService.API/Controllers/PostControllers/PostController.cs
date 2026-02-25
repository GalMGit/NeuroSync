using AggregateService.API.DTOs;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace AggregateService.API.Controllers.PostControllers;

[ApiController]
[Route("aggregate/posts")]
public class PostController(
    IPostServiceClient postServiceClient,
    ICommentServiceClient commentServiceClient
) : BaseController
{
    [HttpGet("{postId:guid}")]
    public async Task<IActionResult> GetPostWithCommentsAsync(Guid postId)
    {
        var postTask = postServiceClient.GetPostByIdAsync(postId);
        var commentsTask = commentServiceClient.GetCommentsByPostAsync(postId);

        await Task.WhenAll(postTask, commentsTask);

        var postResult = postTask.Result;
        var commentsResult = commentsTask.Result;

        if (!postResult.Success && postResult.ErrorCode == "NOT_FOUND")
        {
            return NotFound(new { message = "Пост не найден" });
        }

        if (!postResult.Success)
        {
            return StatusCode(503, new
            {
                message = "Сервис постов временно недоступен",
                errorCode = postResult.ErrorCode
            });
        }

        var postWithComments = new PostWithComments
        {
            Post = postResult.Data!,
            Comments = commentsResult.Success
                ? commentsResult.Data ?? []
                : []
        };

        if (!commentsResult.Success)
        {
            return Ok(new
            {
                message = "Сервис комментариев временно недоступен",
                data = postWithComments
            });
        }

        return Ok(postWithComments);
    }
}