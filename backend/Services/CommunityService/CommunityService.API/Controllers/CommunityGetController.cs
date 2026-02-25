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
        var community = await communityService
            .GetByIdAsync(communityId);

        return Ok(community);
    }
}