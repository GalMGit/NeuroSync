using System;
using MassTransit;
using PostService.CORE.Interfaces.IServices;
using Shared.Messaging.UserEvents;

namespace PostService.BLL.Consumers;

public class UserDeletedConsumer(
    IPostService postService
    ) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;

        await postService
            .SoftDeleteUserPostsAsync(message.UserId);
    }
}
