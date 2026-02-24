using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.WebApi;

namespace CommunityService.API.Controllers;

[ApiController]
[Route("api/communities")]
[Authorize]
public class CommunityPostController : BaseController
{
    
}