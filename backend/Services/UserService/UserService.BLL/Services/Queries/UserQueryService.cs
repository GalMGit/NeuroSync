using System;
using AutoMapper;
using Shared.Contracts.DTOs.User.Responses;
using UserService.CORE.Interfaces.IRepositories;
using UserService.CORE.Interfaces.IServices.IQueries;

namespace UserService.BLL.Services.Queries;

public class UserQueryService(
    IUserRepository userRepository,
    IMapper mapper
    ) : IUserQueryService
{
    public async Task<UserProfileResponse?> GetUserProfileAsync(Guid userId)
    {
        var profile = await userRepository
            .GetByIdAsync(userId)
                ?? throw new Exception("Пользователь не найден");

        return mapper.Map<UserProfileResponse>(profile);
    }
}
