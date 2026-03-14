using System;
using MassTransit;
using PostService.CORE.Interfaces.IServices.IEvents;
using Shared.Messaging.PostEvents;

namespace PostService.BLL.Services.Events;

public class PostEventService(
    IPublishEndpoint publishEndpoint
    ) : IPostEventService
{
    public async Task PublishPostsDeletedAsync(List<Guid> postIds)
    {
        await publishEndpoint.Publish(new PostsDeletedEvent
        {
            PostIds = postIds
        });
    }
}
