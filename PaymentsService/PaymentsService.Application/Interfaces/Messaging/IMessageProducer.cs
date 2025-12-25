using PaymentsService.Application.Outbox;

namespace PaymentsService.Application.Interfaces.Messaging;

public interface IMessageProducer
{
    Task PublishAsync(OutboxMessageDto message, CancellationToken ct);
}
