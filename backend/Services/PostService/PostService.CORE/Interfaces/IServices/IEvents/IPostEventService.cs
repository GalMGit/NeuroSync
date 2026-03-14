using System;

namespace PostService.CORE.Interfaces.IServices.IEvents;

public interface IPostEventService
{
    Task PublishPostsDeletedAsync(List<Guid> postIds);
}
