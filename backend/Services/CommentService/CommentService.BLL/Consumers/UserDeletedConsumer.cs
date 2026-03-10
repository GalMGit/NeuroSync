using System;
using CommentService.CORE.Interfaces.IServices;
using MassTransit;
using Shared.Messaging.UserEvents;

namespace CommentService.BLL.Consumers;

public class UserDeletedConsumer(
    ICommentService commentService
    ) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;

        await commentService
            .SoftDeleteUserCommentsAsync(message.UserId);
    }
}
