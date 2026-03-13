using System;
using AuthService.CORE.Interfaces.IRepositories;
using AuthService.CORE.Interfaces.IServices.IQueries;
using AutoMapper;
using Shared.Contracts.DTOs.Auth.Responses.AuthUserResponse;

namespace AuthService.BLL.Services.Queries;

public class UserQueryService(
    IUserRepository userRepository,
    IMapper mapper
    ) : IUserQueryService
{
    public async Task<List<AuthUserResponse>> GetAllAsync()
    {
        var users = await userRepository
            .GetAllAsync();

        return mapper.Map<List<AuthUserResponse>>(users);
    }

    public async Task<AuthUserResponse?> GetByEmailAsync(string email)
    {
        var user = await userRepository
            .GetByEmailAsync(email);

        return mapper.Map<AuthUserResponse>(user);
    }

    public async Task<AuthUserResponse?> GetByIdAsync(Guid id)
    {
        var user = await userRepository
            .GetByIdAsync(id);

        return mapper.Map<AuthUserResponse>(user);
    }
}
