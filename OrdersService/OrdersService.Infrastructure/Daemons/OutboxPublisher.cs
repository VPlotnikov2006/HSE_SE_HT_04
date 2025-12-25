using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrdersService.Application.Interfaces.Messaging;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Application.Outbox;

namespace OrdersService.Infrastructure.Daemons;

public class OutboxPublisherDaemon(IServiceScopeFactory scopeFactory, IMessageProducer producer) : BackgroundService
{

    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IMessageProducer _producer = producer;
    // private readonly IUnitOfWork _uow = uow;

    private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(2);
    private const int BatchSize = 50;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {

        while (!ct.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var outbox = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            try
            {
                var messages = await outbox.GetUnpublishedAsync(BatchSize, ct);

                if (messages.Count == 0)
                {
                    await Task.Delay(_pollInterval, ct);
                    continue;
                }

                foreach (var message in messages)
                {
                    ct.ThrowIfCancellationRequested();

                    try
                    {
                        await _producer.PublishAsync(
                            new OutboxMessageDto(
                                message.OrderId,
                                message.UserId,
                                message.Debit
                            ),
                            ct
                        );

                        message.Publish();
                        // await _outbox.UpdateAsync(message, ct);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                await uow.CommitAsync(ct);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception)
            {
                await Task.Delay(_pollInterval, ct);
            }
        }
    }
}
