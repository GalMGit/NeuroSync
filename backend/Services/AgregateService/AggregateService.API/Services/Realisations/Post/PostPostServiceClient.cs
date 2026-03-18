using System;
using AggregateService.API.Services.Interfaces.ICommunity;
using AggregateService.API.Services.Interfaces.IPost;
using Shared.Contracts.DTOs.Post.Requests;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Realisations.Post;

public class PostPostServiceClient(
    ICommunityServiceClient communityServiceClient,
    IHttpClientFactory httpClientFactory
    ) : IPostPostServiceClient
{
    private readonly HttpClient _httpClient = httpClientFactory
        .CreateClient("PostService");

    public async Task<PostResponse?> CreatePostAsync(CreatePostRequest request)
    {
        if (request.CommunityId.HasValue)
        {
            var communityExists = await communityServiceClient
                .CheckExistCommunityAsync(request.CommunityId.Value);

            if (communityExists != true)
            {
                throw new KeyNotFoundException($"Сообщество с ID {request.CommunityId} не существует");
            }
        }

        var response = await _httpClient
            .PostAsJsonAsync($"/api/posts", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<PostResponse>();
        }

        return null;
    }
}