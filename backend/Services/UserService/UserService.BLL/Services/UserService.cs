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
            PostCount = 0
        };

        await userRepository
            .CreateAsync(user);
    }

    public async Task<UserProfileResponse?> GetMyProfileAsync(Guid userId)
    {
        var myProfile = await userRepository
            .GetByIdAsync(userId);

        return mapper.Map<UserProfileResponse>(myProfile);
    }

    public async Task<UserProfileResponse?> GetUserProfileAsync(Guid userId)
    {
        var profile = await userRepository
            .GetByIdAsync(userId);

        if(profile is null || profile.IsDeleted)
        {
            throw new Exception("Аккаунт был удален");
        }

        return mapper.Map<UserProfileResponse>(profile);
    }

    public async Task SoftDeleteAsync(Guid userId)
    {
        await userRepository
            .SoftDeleteAsync(userId);
    }
}