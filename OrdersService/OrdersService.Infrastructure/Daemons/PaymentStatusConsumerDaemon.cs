using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersService.Application.Interfaces.Messaging;
using OrdersService.Application.UseCases.UpdateOrderStatus;
using OrdersService.Domain.Orders;

namespace OrdersService.Infrastructure.Daemons;

public sealed class PaymentStatusConsumerDaemon(IMessageConsumer consumer, IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IMessageConsumer _consumer = consumer;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<UpdateOrderStatusHandler>();
        await _consumer.ConsumeAsync(
            async (command, token) =>
            {
                await handler.Handle(
                    new UpdateOrderStatusQuery(
                        command.OrderId,
                        Enum.Parse<OrderStatus>(command.NewStatus, true)
                    ), token);
            },
            ct);
    }
}
