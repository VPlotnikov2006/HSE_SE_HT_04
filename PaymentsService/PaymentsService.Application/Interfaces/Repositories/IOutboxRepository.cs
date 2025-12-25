using PaymentsService.Application.Outbox;

namespace PaymentsService.Application.Interfaces.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message, CancellationToken ct);
    Task<List<OutboxMessage>> GetUnpublishedAsync(uint batchSize, CancellationToken ct);
}
