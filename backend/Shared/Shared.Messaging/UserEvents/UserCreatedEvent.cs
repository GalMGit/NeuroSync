using System;

namespace Shared.Messaging.UserEvents;

public class UserCreatedEvent
{
    public Guid UserId { get;set; }
    public string Username { get;set; }
}
