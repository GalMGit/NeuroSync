using Microsoft.AspNetCore.Identity.Data;
using Shared.Contracts.DTOs.Auth.Responses;
using LoginRequest = Shared.Contracts.DTOs.Auth.Requests.LoginRequest;
using RegisterRequest = Shared.Contracts.DTOs.Auth.Requests.RegisterRequest;

namespace ApiGateway.services;

public class AuthService(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _httpClient =
        clientFactory.CreateClient("AuthService");

    public async Task<string> GetTestAsync()
    {
        var response = await _httpClient.GetAsync("api/test");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        return null;
    }
    
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient
            .PostAsJsonAsync("api/auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<LoginResponse>();

            return result;
        }

        return null;
    }
    
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient
            .PostAsJsonAsync("api/auth/register", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<RegisterResponse>();

            return result;
        }

        return null;
    }
}