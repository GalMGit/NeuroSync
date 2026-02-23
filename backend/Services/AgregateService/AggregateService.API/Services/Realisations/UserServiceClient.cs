using AggregateService.API.Services.Interfaces;
using Shared.Contracts.DTOs.User.Responses;

namespace AggregateService.API.Services.Realisations;

public class UserServiceClient(IHttpClientFactory clientFactory) : IUserServiceClient
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("UserService");
    
    public async Task<UserProfileResponse?> GetProfileAsync()
    {
        var response = await _httpClient
            .GetAsync("/api/user/profile");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<UserProfileResponse>();

            return result;
        }

        return null;
    }
}