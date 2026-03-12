using CommunityService.CORE.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Shared.WebApi;

namespace CommunityService.API.Controllers;

[Route("api/communities")]
[ApiController]
[Authorize]
public class CommunityDeleteController(
    ICommunityService communityService
    ) : BaseController
{
    
}