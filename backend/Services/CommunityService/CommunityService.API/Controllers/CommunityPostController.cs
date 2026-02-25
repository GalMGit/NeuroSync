using CommunityService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Community.Requests;
using Shared.WebApi;

namespace CommunityService.API.Controllers;

[ApiController]
[Route("api/communities")]
[Authorize]
public class CommunityPostController(
    ICommunityService communityService
    ) : BaseController
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(CreateCommunityRequest request)
    {
        var community = await communityService
            .CreateAsync(request, UserId, Username);

        return Ok(community);
    }
}