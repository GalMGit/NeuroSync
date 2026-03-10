using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.CORE.Interfaces.IServices;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.WebApi;

namespace PostService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/posts")]
public class PostPatchController(
    IPostService postService
    ) : BaseController
{
    [HttpPatch("update/{postId:guid}")]
    public async Task<IActionResult> UpdatePostAsync(
        Guid postId,
        UpdatePostRequest request)
    {
        try
        {
            var updatedPost = await postService
                .UpdatePostAsync(postId, request);

            return Ok(updatedPost);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}