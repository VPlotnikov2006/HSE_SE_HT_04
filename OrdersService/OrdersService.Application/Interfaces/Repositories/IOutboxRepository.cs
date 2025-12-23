using OrdersService.Application.Outbox;

namespace OrdersService.Application.Interfaces.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessageDto message, CancellationToken ct);
    Task<List<OutboxMessage>> GetUnpublishedAsync(uint batchSize, CancellationToken ct);
    // Task MarkAsPublishedAsync(Guid orderId, CancellationToken ct);
}
