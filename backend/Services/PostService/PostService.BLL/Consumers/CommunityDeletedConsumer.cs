using System;
using MassTransit;
using PostService.CORE.Interfaces.IServices;
using Shared.Messaging.CommunityEvents;

namespace PostService.BLL.Consumers;

public class CommunityDeletedConsumer(
    IPostService postService
    ) : IConsumer<CommunityDeletedEvent>
{
    public async Task Consume(ConsumeContext<CommunityDeletedEvent> context)
    {
        var message = context.Message;

        await postService
            .SoftDeleteAllByCommunity(message.CommunityId);
    }
}
