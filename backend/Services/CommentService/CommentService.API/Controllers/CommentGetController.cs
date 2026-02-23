using CommentService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace CommentService.API.Controllers;
[ApiController]
[Route("api/comments")]
public class CommentGetController(
    ICommentService commentService
    ) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var comments = await commentService
            .GetAllAsync();

        return Ok(comments);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCommentByIdAsync(Guid id)
    {
        var comment = await commentService
            .GetByIdAsync(id);

        return Ok(comment);
    }

    [HttpGet("post/{postId:guid}")]
    public async Task<IActionResult> GetAllByPostAsync(Guid postId)
    {
        var comments = await commentService
            .GetAllByPostAsync(postId);

        return Ok(comments);
    }
}