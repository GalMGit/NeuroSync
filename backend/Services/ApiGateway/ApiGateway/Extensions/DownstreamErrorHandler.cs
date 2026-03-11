using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Extensions;

public class DownstreamErrorHandler : DelegatingHandler
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is System.Net.Sockets.SocketException)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.6.4",
                Title = "ServiceUnavailable",
                Status = StatusCodes.Status503ServiceUnavailable,
                Detail = "Сервис временно недоступен",
                Instance = request.RequestUri?.PathAndQuery ?? "/"
            };

            var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(problemDetails, _jsonOptions),
                    System.Text.Encoding.UTF8,
                    "application/problem+json"
                )
            };

            return response;
        }
    }
}