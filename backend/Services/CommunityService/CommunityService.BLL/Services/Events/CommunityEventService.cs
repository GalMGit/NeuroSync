using System;
using CommunityService.CORE.Interfaces.IServices.IEvents;
using MassTransit;
using Shared.Messaging.CommunityEvents;

namespace CommunityService.BLL.Services.Events;

public class CommunityEventService(
    IPublishEndpoint publishEndpoint
    ) : ICommunityEventService
{
    public async Task PublishCommunityDeletedAsync(Guid communityId)
    {
        await publishEndpoint
            .Publish(new CommunityDeletedEvent
            {
                CommunityId = communityId
            });
    }
}
