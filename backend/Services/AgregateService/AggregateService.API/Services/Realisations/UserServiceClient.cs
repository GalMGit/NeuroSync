using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using AggregateService.API.DTOs.Errors;
using AggregateService.API.Extensions.Exceptions;
using AggregateService.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.DTOs.User.Responses;

namespace AggregateService.API.Services.Realisations;

public class UserServiceClient(
    IHttpClientFactory clientFactory
    ) : IUserServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("UserService");

    public async Task<UserProfileResponse> GetProfileAsync()
    {
        try
        {
            var response = await _httpClient
                .GetAsync("/api/user/profile");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<UserProfileResponse>()
                    ?? throw new ServiceException(
                        "Пустой ответ от сервиса пользователей",
                        "EmptyResponse",
                        HttpStatusCode.InternalServerError
                    );
            }

            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();

            throw new ServiceException(
                problem?.Detail ?? "Ошибка при получении профиля",
                problem?.Title ?? "UserServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис пользователей временно недоступен",
                "ServiceUnvailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }

        public async Task<UserProfileResponse> GetUserByIdAsync(Guid userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserProfileResponse>()
                       ?? throw new ServiceException(
                           "Пустой ответ от сервиса пользователей",
                           "EmptyResponse",
                           HttpStatusCode.InternalServerError);
            }

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            throw new ServiceException(
                problem?.Detail ?? "Ошибка при получении пользователя",
                problem?.Title ?? "UserServiceError",
                response.StatusCode
            );
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            throw new ServiceException(
                "Сервис пользователей временно недоступен",
                "ServiceUnavailable",
                HttpStatusCode.ServiceUnavailable
            );
        }
    }
}