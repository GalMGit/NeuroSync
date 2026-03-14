using System;

namespace CommunityService.CORE.Interfaces.IServices.IEvents;

public interface ICommunityEventService
{
    Task PublishCommunityDeletedAsync(Guid communityId);
}
