namespace PaymentsService.Application.Interfaces;

public interface IInboxRepository
{
    Task<bool> TryAddAsync(Guid OrderId, CancellationToken ct);
}
