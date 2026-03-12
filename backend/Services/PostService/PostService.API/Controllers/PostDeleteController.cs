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
        try
        {
            await postService
                .SoftDeleteAsync(postId, UserId);

            return Ok("Пост удален");
        }
        catch (UnauthorizedAccessException)
        {
            return Problem(
                title: "Ошибка удаления поста",
                detail: "У вас нет прав на удаление этого поста",
                statusCode: StatusCodes.Status403Forbidden
            );
        }
        catch (Exception e)
        {
             return Problem(
                title: "Ошибка удаления поста",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    }
}