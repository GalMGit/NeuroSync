using System;

namespace Shared.Messaging.PostEvents;

public class PostsDeletedEvent
{
    public List<Guid> PostIds {get;set;} =[];
}