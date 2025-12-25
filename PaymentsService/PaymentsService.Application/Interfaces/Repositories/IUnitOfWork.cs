namespace PaymentsService.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
    Task RollbackAsync(CancellationToken ct);
}
