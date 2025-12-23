using OrdersService.Application.DTOs;

namespace OrdersService.Application.Interfaces.Repositories;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessageDto message, CancellationToken ct);
    Task<List<OutboxMessageDto>> GetUnpublishedAsync(CancellationToken ct);
    Task MarkAsPublishedAsync(Guid orderId, CancellationToken ct);
}
