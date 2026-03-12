using System;

namespace Shared.Messaging.PostEvents;

public class PostDeletedEvent
{
    public Guid PostId {get;set;}
}
