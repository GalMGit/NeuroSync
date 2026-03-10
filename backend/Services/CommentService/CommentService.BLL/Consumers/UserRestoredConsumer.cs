using System;
using CommentService.CORE.Interfaces.IServices;
using MassTransit;
using Shared.Messaging.UserEvents;

namespace CommentService.BLL.Consumers;

public class UserRestoredConsumer(
    ICommentService commentService
    ) : IConsumer<UserRestoredEvent>
{
    public async Task Consume(ConsumeContext<UserRestoredEvent> context)
    {
        var message = context.Message;

        await commentService
            .RestoreDeletesUserCommentsAsync(message.UserId);
    }
}
