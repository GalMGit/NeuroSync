using System;
using CommentService.CORE.Interfaces.IServices;
using MassTransit;
using Shared.Messaging.PostEvents;

namespace CommentService.BLL.Consumers;

public class PostsDeletedConsumer(
    ICommentService commentService
    ) : IConsumer<PostsDeletedEvent>
{
    public async Task Consume(ConsumeContext<PostsDeletedEvent> context)
    {
        var message = context.Message;

        await commentService
            .SoftDeleteAllByPostIdsAsync(message.PostIds);
    }
}
