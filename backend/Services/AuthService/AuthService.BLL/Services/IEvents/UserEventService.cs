using System;
using AuthService.CORE.Entities;
using AuthService.CORE.Interfaces.IServices.IEvents;
using MassTransit;
using Shared.Messaging.UserEvents;

namespace AuthService.BLL.Services.IEvents;

public class UserEventService(
    IPublishEndpoint publishEndpoint
    ) : IUserEventService
{
    public async Task PublishUserCreatedAsync(Guid userId, string username)
    {
        await publishEndpoint.Publish(new UserCreatedEvent
        {
            UserId = userId,
            Username = username
        });
    }

    public async Task PublishUserDeletedAsync(Guid userId)
    {
        await publishEndpoint.Publish(new UserDeletedEvent
        {
            UserId = userId
        });
    }

    public async Task PublishUserRestoredAsync(Guid userId)
    {
        await publishEndpoint.Publish(new UserRestoredEvent
        {
            UserId = userId
        });
    }
}
