using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Realisations;

public class CommentServiceClient(
    IHttpClientFactory clientFactory
    ) : ICommentServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("CommentService");
    
    public async Task<IEnumerable<CommentResponse>?> GetCommentsByPostAsync(Guid postId)
    {
        var response = await _httpClient
            .GetAsync($"api/comments/post/{postId}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<IEnumerable<CommentResponse>>();

            return result ?? [];
        }

        return [];
    }
}