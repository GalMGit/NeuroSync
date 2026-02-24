using AggregateService.API.DTOs.Errors;
using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Realisations;

public class CommentServiceClient(
    IHttpClientFactory clientFactory
) : ICommentServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("CommentService");
    
    public async Task<ServiceResponse<IEnumerable<CommentResponse>>> GetCommentsByPostAsync(Guid postId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"api/comments/post/{postId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<IEnumerable<CommentResponse>>();

                return ServiceResponse<IEnumerable<CommentResponse>>.Ok(result ?? []);
            }
            
            return ServiceResponse<IEnumerable<CommentResponse>>.Ok([], "Комментарии не найдены");
        }
        catch (HttpRequestException ex) when (ex.InnerException is System.Net.Sockets.SocketException)
        {
            return ServiceResponse<IEnumerable<CommentResponse>>.ServiceUnavailable("комментариев");
        }
        catch (TaskCanceledException)
        {
            return ServiceResponse<IEnumerable<CommentResponse>>.Timeout("комментариев");
        }
    }
}