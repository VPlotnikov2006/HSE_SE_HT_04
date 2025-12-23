using OrdersService.Application.DTOs;

namespace OrdersService.Application.Interfaces.Messaging;

public interface IMessageConsumer
{
    Task ConsumeAsync(
        Func<UpdateOrderStatusCommand, CancellationToken, Task> handler,
        CancellationToken ct);
}
