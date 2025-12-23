using Microsoft.Extensions.Hosting;
using OrdersService.Application.Interfaces.Messaging;
using OrdersService.Application.UseCases.UpdateOrderStatus;
using OrdersService.Domain.Orders;

namespace OrdersService.Infrastructure.Daemons;

public sealed class PaymentStatusConsumerDaemon(IMessageConsumer consumer, UpdateOrderStatusHandler handler) : BackgroundService
{
    private readonly IMessageConsumer _consumer = consumer;
    private readonly UpdateOrderStatusHandler _handler = handler;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await _consumer.ConsumeAsync(
            async (command, token) =>
            {
                await _handler.Handle(
                    new UpdateOrderStatusQuery(
                        command.OrderId,
                        Enum.Parse<OrderStatus>(command.NewStatus, true)
                    ), token);
            },
            ct);
    }
}
