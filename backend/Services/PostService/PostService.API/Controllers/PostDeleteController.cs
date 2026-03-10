using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.CORE.Interfaces.IServices;
using Shared.WebApi;

namespace PostService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/posts")]
public class PostDeleteController(
    IPostService postService
    ) : BaseController
{
    [HttpDelete("delete/{postId:guid}")]
    public async Task<IActionResult> SoftDeletePostAsync(Guid postId)
    {
        await postService
            .SoftDeleteAsync(postId);

        return Ok("Пост удален");
    }
}