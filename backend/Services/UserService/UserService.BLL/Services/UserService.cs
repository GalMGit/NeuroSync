using AutoMapper;
using Shared.Contracts.DTOs.User.Responses;
using Shared.Messaging.UserEvents;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IRepositories;
using UserService.CORE.Interfaces.IServices;

namespace UserService.BLL.Services;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper
) : IUserService
{
    public async Task CreateUserInfoAsync(UserCreatedEvent @event)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            DisplayName = @event.Username,
            UserId = @event.UserId,
        };

        await userRepository
            .CreateAsync(user);
    }

    public async Task<UserProfileResponse?> GetUserProfileAsync(Guid userId)
    {
        var profile = await userRepository
            .GetByIdAsync(userId)
                ?? throw new Exception("Пользователь не найден");


        return mapper.Map<UserProfileResponse>(profile);
    }

    public async Task RestoreAccountAsync(Guid userId)
    {
        await userRepository
            .RestoreUserAsync(userId);
    }

    public async Task SoftDeleteAsync(Guid userId)
    {
        await userRepository
            .SoftDeleteAsync(userId);
    }
}