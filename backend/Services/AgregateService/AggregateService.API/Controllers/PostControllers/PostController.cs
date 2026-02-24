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

        var postResult = await postTask;
        var commentsResult = await commentsTask;

        var postsWithComments = new PostWithComments
        {
            Post = postResult.Success ? postResult.Data : null,
            Comments = commentsResult.Success ? commentsResult.Data ?? [] : []
        };
        
        if (!postResult.Success && postResult.ErrorCode == "NOT_FOUND")
        {
            return NotFound(new { message = "Пост не найден" });
        }
        
        if (!postResult.Success && !commentsResult.Success)
        {
            return StatusCode(503, new { 
                message = "Сервисы временно недоступны",
                details = postsWithComments 
            });
        }
        
        return Ok(postsWithComments);
    }
}