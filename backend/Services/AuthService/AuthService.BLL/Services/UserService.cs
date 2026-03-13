using AuthService.CORE.Interfaces.IServices;
using AuthService.CORE.Interfaces.IServices.ICommands;
using AuthService.CORE.Interfaces.IServices.IQueries;
using Shared.Contracts.DTOs.Auth.Requests;
using Shared.Contracts.DTOs.Auth.Responses;
using Shared.Contracts.DTOs.Auth.Responses.AuthUserResponse;

namespace AuthService.BLL.Services;

public class UserService(
    IUserCommandService commandService,
    IUserQueryService queryService
    ) : IUserService
{
    public async Task<RegisterResponse> CreateAsync(RegisterRequest request)
        => await commandService.CreateAsync(request);

    public async Task<List<AuthUserResponse>> GetAllAsync()
        => await queryService.GetAllAsync();

    public async Task<AuthUserResponse?> GetByEmailAsync(string email)
        => await queryService.GetByEmailAsync(email);

    public async Task<AuthUserResponse?> GetByIdAsync(Guid id)
        => await queryService.GetByIdAsync(id);

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
        => await commandService.LoginAsync(request);

    public async Task RestoreAccountAsync(LoginRequest request)
        => await commandService.RestoreAccountAsync(request);

    public async Task SoftDeleteAsync(Guid userId)
        => await commandService.SoftDeleteAsync(userId);
}