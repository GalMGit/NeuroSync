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
                .UpdatePostAsync(
                    postId,
                    UserId,
                    request);

            return Ok(updatedPost);
        }
        catch(UnauthorizedAccessException)
        {
            return Problem(
                title: "Ошибка обновления поста",
                detail: "У вас нет прав на изменение этого поста",
                statusCode: StatusCodes.Status403Forbidden
            );
        }
        catch (Exception e)
        {
            return Problem(
                title: "Ошибка обновления поста",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    }

    [HttpPatch("delete/{postId:guid}")]
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