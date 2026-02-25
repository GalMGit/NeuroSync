using System;
using System.Net;
using System.Net.Sockets;
using AggregateService.API.DTOs.Errors;
using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.Community.Responses;

namespace AggregateService.API.Services.Realisations;

public class CommunityServiceClient(
    IHttpClientFactory clientFactory
) : ICommunityServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("CommunityService");

    public async Task<ServiceResponse<CommunityResponse>> GetCommunityAsync(Guid communityId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"api/communities/{communityId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<CommunityResponse>();

                return ServiceResponse<CommunityResponse>
                    .Ok(result!);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return ServiceResponse<CommunityResponse>
                    .Fail("сообщество не найдено", "NOT_FOUND");
            }

            return ServiceResponse<CommunityResponse>
                .Fail("Ошибка при получении сообщества",
                    "COMMUNITY_SERVICE_ERROR");
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException)
        {
            return ServiceResponse<CommunityResponse>
                .ServiceUnavailable("сообществ");
        }
    }
}