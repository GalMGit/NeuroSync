using System;
using CommunityService.CORE.Interfaces.IServices;
using MassTransit;
using Shared.Messaging.UserEvents;

namespace CommunityService.BLL.Consumers;

public class UserRestoredConsumer(
    ICommunityService communityService
    ) : IConsumer<UserRestoredEvent>
{
    public async Task Consume(ConsumeContext<UserRestoredEvent> context)
    {
        var message = context.Message;

        await communityService
            .RestoreDeletedUserCommunities(message.UserId);
    }
}
