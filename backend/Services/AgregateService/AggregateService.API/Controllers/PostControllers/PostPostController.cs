using System;
using AggregateService.API.Services.Interfaces.IPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.WebApi;

namespace AggregateService.API.Controllers.PostControllers;

[ApiController]
[Authorize]
[Route("aggregate/posts")]
public class PostPostController(
    IPostPostServiceClient postPostService
    ) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreatePostAsync(CreatePostRequest request)
    {
        try
        {
            var post = await postPostService
            .CreatePostAsync(request);

            return Ok(post);
        }
        catch(KeyNotFoundException e)
        {
            return Problem(
                title: "Ошибка при создании поста",
                detail: e.Message,
                statusCode: StatusCodes.Status404NotFound
            );
        }
        catch(Exception e)
        {
            return Problem(
                title: "Ошибка при создании поста",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    }
}
