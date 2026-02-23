namespace AggregateService.API.Extensions;

public class TokenPropagationHandler(
    IHttpContextAccessor contextAccessor
    ) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var context = contextAccessor.HttpContext;
        if (context != null)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.TryAddWithoutValidation("Authorization", token);
            }
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}