using System;
using Shared.Messaging.UserEvents;

namespace UserService.CORE.Interfaces.IServices.ICommands;

public interface IUserCommandService
{
    Task CreateUserInfoAsync(UserCreatedEvent @event);
    Task SoftDeleteAsync(Guid userId);
    Task RestoreAccountAsync(Guid userId);
}
