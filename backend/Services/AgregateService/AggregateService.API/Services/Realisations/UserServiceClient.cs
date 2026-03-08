using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using AggregateService.API.DTOs.Errors;
using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.User.Responses;

namespace AggregateService.API.Services.Realisations;

public class UserServiceClient(
    IHttpClientFactory clientFactory
    ) : IUserServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("UserService");

    public async Task<ServiceResponse<UserProfileResponse>> GetProfileAsync()
    {
        try
        {
            var response = await _httpClient
                .GetAsync("/api/user/profile");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<UserProfileResponse>();
                return ServiceResponse<UserProfileResponse>.Ok(result!);
            }

            var errorContent = await response.Content
                .ReadAsStringAsync();

            var (message, errorCode) = ParseErrorResponse(errorContent);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    if (errorCode == "USER_DELETED")
                    {
                        return ServiceResponse<UserProfileResponse>.UserDeleted(
                            message ?? "Пользователь был удален"
                        );
                    }
                    return ServiceResponse<UserProfileResponse>.NotFound(
                        message ?? "Пользователь не найден"
                    );

                case HttpStatusCode.Unauthorized:
                    return ServiceResponse<UserProfileResponse>.Unauthorized();

                case HttpStatusCode.BadRequest:
                    return ServiceResponse<UserProfileResponse>.Fail(
                        message ?? "Ошибка запроса",
                        errorCode ?? "BAD_REQUEST"
                    );

                default:
                    return ServiceResponse<UserProfileResponse>.Fail(
                        message ?? "Не удалось получить профиль пользователя",
                        errorCode ?? "USER_SERVICE_ERROR"
                    );
            }
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException)
        {
            return ServiceResponse<UserProfileResponse>
                .ServiceUnavailable("пользователей");
        }
        catch (Exception)
        {
            return ServiceResponse<UserProfileResponse>.Fail(
                "Внутренняя ошибка при получении профиля",
                "INTERNAL_ERROR"
            );
        }
    }

    public async Task<ServiceResponse<UserProfileResponse>> GetUserByIdAsync(Guid userId)
    {
        try
        {
            var response = await _httpClient
                .GetAsync($"/api/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<UserProfileResponse>();
                return ServiceResponse<UserProfileResponse>.Ok(result!);
            }

            var errorContent = await response.Content
                .ReadAsStringAsync();

            var (message, errorCode) = ParseErrorResponse(errorContent);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    if (errorCode == "USER_DELETED")
                    {
                        return ServiceResponse<UserProfileResponse>.UserDeleted(
                            message ?? "Пользователь был удален"
                        );
                    }
                    return ServiceResponse<UserProfileResponse>.NotFound(
                        message ?? "Пользователь не найден"
                    );

                case HttpStatusCode.Unauthorized:
                    return ServiceResponse<UserProfileResponse>.Unauthorized();

                case HttpStatusCode.BadRequest:
                    return ServiceResponse<UserProfileResponse>.Fail(
                        message ?? "Ошибка запроса",
                        errorCode ?? "BAD_REQUEST"
                    );

                default:
                    return ServiceResponse<UserProfileResponse>.Fail(
                        message ?? "Не удалось получить профиль пользователя",
                        errorCode ?? "USER_SERVICE_ERROR"
                    );
            }
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException)
        {
            return ServiceResponse<UserProfileResponse>
                .ServiceUnavailable("пользователей");
        }
        catch (Exception)
        {
            return ServiceResponse<UserProfileResponse>.Fail(
                "Внутренняя ошибка при получении профиля",
                "INTERNAL_ERROR"
            );
        }
    }

    private (string? message, string? errorCode) ParseErrorResponse(string errorContent)
    {
        if (string.IsNullOrEmpty(errorContent))
            return (null, null);

        try
        {
            using var doc = JsonDocument.Parse(errorContent);
            string? message = null;
            string? errorCode = null;

            if (doc.RootElement.TryGetProperty("message", out var messageElement))
            {
                message = messageElement.GetString();
            }

            if (doc.RootElement.TryGetProperty("errorCode", out var errorCodeElement))
            {
                errorCode = errorCodeElement.GetString();
            }

            return (message, errorCode);
        }
        catch
        {
            return (errorContent, null);
        }
    }
}