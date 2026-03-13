using AggregateService.API.Services.Interfaces.IComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Comment.Requests;
using Shared.WebApi;

namespace AggregateService.API.Controllers.CommentControllers;

[Route("aggregate/comments")]
[ApiController]
[Authorize]
public class CommentPostController(
    ICommentPostServiceClient commentPostServiceClient
    ) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(CreateCommentRequest request)
    {
        try
        {
            var comment = await commentPostServiceClient
                .CreateCommentAsync(request);

            return Ok(comment);

        }
        catch (KeyNotFoundException e)
        {
            return Problem(
               title: "Ошибка при создании комментария",
               detail: e.Message,
               statusCode: StatusCodes.Status404NotFound
           );
        }
        catch (Exception e)
        {
            return Problem(
               title: "Ошибка при создании поста",
               detail: e.Message,
               statusCode: StatusCodes.Status400BadRequest
           );
        }
    }
}
