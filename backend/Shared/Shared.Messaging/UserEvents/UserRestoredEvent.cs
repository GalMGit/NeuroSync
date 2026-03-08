using System;

namespace Shared.Messaging.UserEvents;

public class UserRestoredEvent
{
    public Guid UserId { get; set; }
}
