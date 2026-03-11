using AggregateService.API.DTOs;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Comment.Responses;
using Shared.WebApi;

namespace AggregateService.API.Controllers.PostControllers;

[ApiController]
[Route("aggregate/posts")]
public class PostController(
    IPostServiceClient postServiceClient,
    ICommentServiceClient commentServiceClient,
    ILogger<PostController> logger
) : BaseController
{
    [HttpGet("{postId:guid}")]
    public async Task<IActionResult> GetPostWithCommentsAsync(Guid postId)
    {
        try
        {

            var post = await postServiceClient
                .GetPostByIdAsync(postId);

            if (post == null)
            {
                return Problem(
                    title: "Пост не найден",
                    detail: $"Пост с идентификатором {postId} не найден",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            IEnumerable<CommentResponse> comments = [];

            try
            {
                comments = await commentServiceClient
                    .GetCommentsByPostAsync(postId);
            }
            catch (ServiceException ex)
            {
            }

            var postWithComments = new PostWithComments
            {
                Post = post,
                Comments = comments ?? []
            };

            return Ok(postWithComments);
        }
        catch (ServiceException ex)
        {
            return Problem(
                title: ex.Title,
                detail: ex.Message,
                statusCode: (int)ex.StatusCode
            );
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Внутренняя ошибка сервера",
                detail: "Произошла непредвиденная ошибка",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}