using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces.ICommunity;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.Community.Responses;

namespace AggregateService.API.Services.Realisations.Community;

public class CommunityServiceClient(
    IHttpClientFactory clientFactory
) : ICommunityServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("CommunityService");

    public async Task<bool?> CheckExistCommunityAsync(Guid? communityId)
    {
        var response = await _httpClient
            .GetAsync($"/api/communities/exist/{communityId}");

        if(response.IsSuccessStatusCode)
        {
            return await response.Content
                .ReadFromJsonAsync<bool>();
        }

        return null;
    }

    public async Task<CommunityResponse?> GetCommunityAsync(Guid communityId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"/api/communities/{communityId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<CommunityResponse>();
            }

            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            throw new ServiceException(
                problem?.Detail ?? $"Ошибка при получении сообщества {communityId}",
                problem?.Title ?? "CommunityServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис сообществ временно недоступен",
                "ServiceUnavailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }
}