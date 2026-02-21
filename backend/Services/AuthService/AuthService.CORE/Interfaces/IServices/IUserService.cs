using Shared.Contracts.DTOs.Auth.Requests;
using Shared.Contracts.DTOs.Auth.Responses;

namespace AuthService.CORE.Interfaces.IServices;

public interface IUserService
{
    Task<RegisterResponse> CreateAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}