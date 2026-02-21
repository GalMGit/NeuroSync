using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroSync.MinimalApi.Endpoints;

namespace AuthService.API.Features.User;

public class TestEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/test", (
            [FromHeader(Name = "X-User-Id")] string userId,
            [FromHeader(Name = "X-Username")] string username) 
                => Results.Ok(username));
    }
}