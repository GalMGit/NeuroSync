using Shared.Contracts.DTOs.User.Responses;
using Shared.Messaging.UserEvents;

namespace UserService.CORE.Interfaces.IServices;

public interface IUserService
{
    Task CreateUserInfoAsync(UserCreatedEvent @event);
    Task<UserProfileResponse?> GetUserProfileAsync(Guid userId);
    Task SoftDeleteAsync(Guid userId);
    Task RestoreAccountAsync(Guid userId);
}