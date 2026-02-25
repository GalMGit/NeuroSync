using System.Net.Sockets;
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

            return ServiceResponse<UserProfileResponse>
                .Fail("Не удалось получить профиль пользователя",
                    "USER_SERVICE_ERROR"
            );
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
}