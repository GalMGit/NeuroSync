using System;
using CommunityService.CORE.Interfaces.IServices;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.UserEvents;

namespace CommunityService.BLL.Consumers;

public class UserDeletedConsumer(
    ICommunityService communityService
    ) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;

        await communityService
            .SoftDeleteUserCommunities(message.UserId);
    }
}