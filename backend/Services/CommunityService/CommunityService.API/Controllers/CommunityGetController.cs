using CommunityService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace CommunityService.API.Controllers;

[ApiController]
[Route("api/communities")]
public class CommunityGetController(
    ICommunityService communityService
    ) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var communities = await communityService
            .GetAllAsync();

        return Ok(communities);
    }

    [HttpGet("exist/{communityId:guid}")]
    public async Task<IActionResult> CheckCommunityExistAsync(Guid communityId)
    {
        var result = await communityService
            .CommunityExistAsync(communityId);

        return Ok(result);
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetAllByUserAsync()
    {
        var communities = await communityService
            .GetAllByUserAsync(UserId);

        return Ok(communities);
    }

    [HttpGet("{communityId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(Guid communityId)
    {
        try
        {
            var community = await communityService
            .GetByIdAsync(communityId);

            return Ok(community);
        }
        catch(KeyNotFoundException e)
        {
            return Problem(
                title: "Ошибка при получении сообщества",
                detail: e.Message,
                statusCode: StatusCodes.Status404NotFound
            );
        }
        catch(Exception e)
        {
            return Problem(
                title: "Ошибка при получении сообщества",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest
            );
        }

    }
}