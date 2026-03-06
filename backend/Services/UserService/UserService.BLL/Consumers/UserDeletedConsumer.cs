using MassTransit;
using Shared.Messaging.UserEvents;
using UserService.CORE.Interfaces.IServices;

namespace UserService.BLL.Consumers;

public class UserDeletedConsumer(
    IUserService userService
    ) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var message = context.Message;

        await userService
            .SoftDeleteAsync(message.UserId);
    }
}
