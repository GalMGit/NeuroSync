using System.Security.Claims;

namespace ApiGateway.Handlers;

public class UserContextHandler(
    IHttpContextAccessor contextAccessor
) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var httpContext = contextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userId = httpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = httpContext.User
                .FindFirst(ClaimTypes.Name)?.Value;
            
            if (!string.IsNullOrEmpty(userId))
                request.Headers.Add("X-User-Id", userId);
                
            if (!string.IsNullOrEmpty(username))
                request.Headers.Add("X-Username", username);
        }
        return base.SendAsync(request, cancellationToken);
    }
}