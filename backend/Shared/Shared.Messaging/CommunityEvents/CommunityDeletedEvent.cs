using System;

namespace Shared.Messaging.CommunityEvents;

public class CommunityDeletedEvent
{
    public Guid CommunityId { get; set; }
}
