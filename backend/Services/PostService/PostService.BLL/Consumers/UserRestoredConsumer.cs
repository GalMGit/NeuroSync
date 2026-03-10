using System;
using MassTransit;
using PostService.CORE.Interfaces.IServices;
using Shared.Messaging.UserEvents;

namespace PostService.BLL.Consumers;

public class UserRestoredConsumer(
    IPostService postService
    ) : IConsumer<UserRestoredEvent>
{
    public async Task Consume(ConsumeContext<UserRestoredEvent> context)
    {
        var message = context.Message;

        await postService
            .RestoreUserPostsAsync(message.UserId);
    }
}
