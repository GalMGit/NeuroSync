using System.Net;
using System.Text.Json;

namespace ApiGateway.Extensions;

public class DownstreamErrorHandler : DelegatingHandler
{
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
            var response = new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = "Сервис временно недоступен",
                        data = Array.Empty<object>()
                    }),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            };
            
            return response;
        }
    }
}