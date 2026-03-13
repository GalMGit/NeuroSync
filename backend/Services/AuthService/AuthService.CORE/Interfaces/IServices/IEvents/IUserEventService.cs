using System;

namespace AuthService.CORE.Interfaces.IServices.IEvents;

public interface IUserEventService
{
    Task PublishUserCreatedAsync(Guid userId, string username);
    Task PublishUserDeletedAsync(Guid userId);
    Task PublishUserRestoredAsync(Guid userId);
}
