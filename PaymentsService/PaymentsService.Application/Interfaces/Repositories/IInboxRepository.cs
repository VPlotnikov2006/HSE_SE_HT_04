using PaymentsService.Application.Inbox;

namespace PaymentsService.Application.Interfaces.Repositories;

public interface IInboxRepository
{
    Task<bool> TryAddAsync(InboxMessage message, CancellationToken ct);
}
