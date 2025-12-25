using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentsService.Application.Interfaces.Messaging;
using PaymentsService.Application.UseCases;

namespace PaymentsService.Infrastructure.Daemons;

public sealed class OrdersOutboxConsumerDaemon(
    IMessageConsumer consumer,
    IServiceScopeFactory scopeFactory
) : BackgroundService
{
    private readonly IMessageConsumer _consumer = consumer;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await _consumer.ConsumeAsync(async (orderId, command, ct) =>
        {
            using var scope = _scopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<WithdrawHandler>();

            await handler.Handle(
                orderId,
                command,
                ct
            );

        }, ct);
    }
}
