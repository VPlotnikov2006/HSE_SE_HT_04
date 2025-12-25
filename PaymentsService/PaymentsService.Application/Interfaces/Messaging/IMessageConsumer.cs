using PaymentsService.Application.DTOs;

namespace PaymentsService.Application.Interfaces.Messaging;

public interface IMessageConsumer
{
    Task ConsumeAsync(
        Func<Guid, UpdateBalanceCommand, CancellationToken, Task> handler,
        CancellationToken ct);
}
