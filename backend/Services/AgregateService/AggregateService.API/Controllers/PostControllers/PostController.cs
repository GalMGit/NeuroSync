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
        var post = await postServiceClient
            .GetPostByIdAsync(postId);

        var comments = await commentServiceClient
            .GetCommentsByPostAsync(postId);

        var postsWithComments = new PostWithComments
        {
            Comments = comments,
            Post = post
        };
        
        return Ok(postsWithComments);
    }
}