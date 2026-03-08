using System;
using MassTransit;
using Shared.Messaging.UserEvents;
using UserService.CORE.Interfaces.IServices;

namespace UserService.BLL.Consumers;

public class UserRestoredConsumer(
    IUserService userService
    ) : IConsumer<UserRestoredEvent>
{
    public async Task Consume(ConsumeContext<UserRestoredEvent> context)
    {
        var message = context.Message;

        await userService
            .RestoreAccountAsync(message.UserId);
    }
}
