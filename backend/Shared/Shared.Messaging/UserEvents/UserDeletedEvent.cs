using System;

namespace Shared.Messaging.UserEvents;

public class UserDeletedEvent
{
    public Guid UserId { get;set; }
}
