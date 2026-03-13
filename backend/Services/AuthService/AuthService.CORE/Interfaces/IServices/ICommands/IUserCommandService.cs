using System;
using Shared.Contracts.DTOs.Auth.Requests;
using Shared.Contracts.DTOs.Auth.Responses;

namespace AuthService.CORE.Interfaces.IServices.ICommands;

public interface IUserCommandService
{
    Task<RegisterResponse> CreateAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task RestoreAccountAsync(LoginRequest request);
    Task SoftDeleteAsync(Guid userId);
}
