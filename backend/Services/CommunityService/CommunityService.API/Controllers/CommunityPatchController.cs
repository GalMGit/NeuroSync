using System;
using CommunityService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace CommunityService.API.Controllers;

[ApiController]
[Route("api/communities")]
[Authorize]
public class CommunityPatchController(
    ICommunityService communityService
    ) : BaseController
{
    [HttpPatch("{communityId:guid}")]
    public async Task<IActionResult> SoftDeleteCommunityAsync(Guid communityId)
    {
        try
        {
            await communityService
                .SoftDeleteAsync(communityId);

            return Ok("Сообщество удалено");
        }
        catch(KeyNotFoundException e)
        {
            return Problem(
                title: "Ошибка при удалении сообщества",
                detail: e.Message,
                statusCode: StatusCodes.Status404NotFound
            );
        }
        catch(Exception e)
        {
            return Problem(
                title: "Ошибка при удалении сообщества",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }
    }
}
