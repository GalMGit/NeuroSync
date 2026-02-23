using CommentService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.WebApi;

namespace CommentService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/comments")]
public class CommentPostController(
    ICommentService commentService
    ) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(CreateCommentRequest request)
    {
        var comment = await commentService
            .CreateAsync(request, UserId, Username);

        return Ok(comment);
    }
}