using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Realisations;

public class PostServiceClient(
    IHttpClientFactory clientFactory
    ) : IPostServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("PostService");
    
    public async Task<IEnumerable<PostResponse>?> GetUserPostsAsync()
    {
        var response = await _httpClient.GetAsync("/api/posts/user");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<IEnumerable<PostResponse>>();

            return result ?? [];
        }

        return [];
    }
    
    public async Task<PostResponse?> GetPostByIdAsync(Guid postId)
    {
        var response = await _httpClient.GetAsync($"/api/posts/{postId}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<PostResponse>();

            return result;
        }

        return null;
    }
}