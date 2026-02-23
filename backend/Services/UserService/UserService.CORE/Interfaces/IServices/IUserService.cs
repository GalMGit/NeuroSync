using Shared.Contracts.DTOs.User.Responses;
using Shared.Messaging.UserEvents;

namespace UserService.CORE.Interfaces.IServices;

public interface IUserService
{
    Task CreateUserInfoAsync(UserCreatedEvent @event);
    Task<UserProfileResponse?> GetMyProfileAsync(Guid userId);
    Task<UserProfileResponse?> GetUserProfileAsync(Guid userId);
}