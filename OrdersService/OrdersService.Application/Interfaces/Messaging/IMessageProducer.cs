using OrdersService.Application.Outbox;

namespace OrdersService.Application.Interfaces.Messaging;

public interface IMessageProducer
{
    Task PublishAsync(OutboxMessageDto message, CancellationToken ct);
}
