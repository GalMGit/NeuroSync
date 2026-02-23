using MassTransit;
using Shared.Messaging.UserEvents;
using UserService.CORE.Entities;
using UserService.CORE.Interfaces.IServices;

namespace UserService.BLL.Consumers;

public class UserCreatedConsumer(
    IUserService userService
    ) : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;

        await userService
            .CreateUserInfoAsync(message);
    }
}