using System;
using Shared.Messaging.UserEvents;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IRepositories;
using UserService.CORE.Interfaces.IServices.ICommands;

namespace UserService.BLL.Services.Commands;

public class UserCommandService(
    IUserRepository userRepository
    ) : IUserCommandService
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
