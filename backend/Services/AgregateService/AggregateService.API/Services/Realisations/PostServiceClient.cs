using AggregateService.API.DTOs.Errors;
using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Realisations;

public class PostServiceClient(
    IHttpClientFactory clientFactory
    ) : IPostServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("PostService");
    
    public async Task<ServiceResponse<IEnumerable<PostResponse>>> GetUserPostsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/posts/user");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<IEnumerable<PostResponse>>();

                return ServiceResponse<IEnumerable<PostResponse>>.Ok(result ?? []);
            }
            
            return ServiceResponse<IEnumerable<PostResponse>>.Ok([], "Посты не найдены");
        }
        catch (HttpRequestException ex) when (ex.InnerException is System.Net.Sockets.SocketException)
        {
            return ServiceResponse<IEnumerable<PostResponse>>.ServiceUnavailable("постов");
        }
        catch (TaskCanceledException)
        {
            return ServiceResponse<IEnumerable<PostResponse>>.Timeout("постов");
        }
    }
    
    public async Task<ServiceResponse<PostResponse>> GetPostByIdAsync(Guid postId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/posts/{postId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<PostResponse>();

                return ServiceResponse<PostResponse>.Ok(result!);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return ServiceResponse<PostResponse>.Fail("Пост не найден", "NOT_FOUND");
            }

            return ServiceResponse<PostResponse>.Fail("Ошибка при получении поста", "POST_SERVICE_ERROR");
        }
        catch (HttpRequestException ex) when (ex.InnerException is System.Net.Sockets.SocketException)
        {
            return ServiceResponse<PostResponse>.ServiceUnavailable("постов");
        }
        catch (TaskCanceledException)
        {
            return ServiceResponse<PostResponse>.Timeout("постов");
        }
    }
}