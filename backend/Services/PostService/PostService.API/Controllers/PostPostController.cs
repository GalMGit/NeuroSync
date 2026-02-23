using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.CORE.Interfaces.IServices;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.WebApi;

namespace PostService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/posts")]
public class PostPostController(
    IPostService postService
    ) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreatePostAsync(CreatePostRequest request)
    {
        var post = await postService
            .CreateAsync(request, UserId);

        return Ok(post);
    }
}