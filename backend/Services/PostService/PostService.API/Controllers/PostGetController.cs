using Microsoft.AspNetCore.Mvc;
using PostService.CORE.Interfaces.IServices;
using Shared.WebApi;

namespace PostService.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostGetController(
    IPostService postService
    ) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var posts = await postService
            .GetAllAsync();

        return Ok(posts);
    }

    [HttpGet("{postId:guid}")]
    public async Task<IActionResult> GetPostByIdAsync(Guid postId)
    {
        var post = await postService
            .GetByIdAsync(postId);

        return Ok(post);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetMyPostsAsync()
    {
        var posts = await postService
            .GetAllByUserAsync(UserId);

        return Ok(posts);
    }

    [HttpGet("community/{communityId:guid}")]
    public async Task<IActionResult> GetAllByCommunityAsync(Guid communityId)
    {
        var posts = await postService
            .GetAllByCommunityAsync(communityId);

        return Ok(posts);
    }
}