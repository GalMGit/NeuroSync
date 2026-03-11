using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Comment.Responses;

namespace AggregateService.API.Services.Realisations;

public class CommentServiceClient(
    IHttpClientFactory clientFactory
) : ICommentServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("CommentService");

    public async Task<IEnumerable<CommentResponse>> GetCommentsByPostAsync(Guid postId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"/api/comments/post/{postId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<IEnumerable<CommentResponse>>()
                       ?? [];
            }

            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();

            throw new ServiceException(
                problem?.Detail ?? $"Ошибка при получении комментариев для поста {postId}",
                problem?.Title ?? "CommentServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис комментариев временно недоступен",
                "ServiceUnavailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }
}