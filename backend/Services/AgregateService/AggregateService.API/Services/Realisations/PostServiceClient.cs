using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Post.Responses;

namespace AggregateService.API.Services.Realisations;

public class PostServiceClient(
    IHttpClientFactory clientFactory
) : IPostServiceClient
{
    private readonly HttpClient _httpClient = clientFactory
        .CreateClient("PostService");

    public async Task<IEnumerable<PostResponse>> GetUserPostsAsync()
    {
        try
        {
            var response = await _httpClient
                .GetAsync("/api/posts/user");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<IEnumerable<PostResponse>>()
                       ?? [];
            }

            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();

            throw new ServiceException(
                problem?.Detail ?? "Ошибка при получении постов пользователя",
                problem?.Title ?? "PostServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис постов временно недоступен",
                "ServiceUnavailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }

    public async Task<IEnumerable<PostResponse>> GetPostsByCommunityAsync(Guid communityId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"/api/posts/community/{communityId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<IEnumerable<PostResponse>>()
                       ?? [];
            }

            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();

            throw new ServiceException(
                problem?.Detail ?? $"Ошибка при получении постов сообщества {communityId}",
                problem?.Title ?? "PostServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис постов временно недоступен",
                "ServiceUnavailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }

    public async Task<PostResponse?> GetPostByIdAsync(Guid postId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"/api/posts/{postId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<PostResponse>();
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();

            throw new ServiceException(
                problem?.Detail ?? $"Ошибка при получении поста {postId}",
                problem?.Title ?? "PostServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис постов временно недоступен",
                "ServiceUnavailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }
}