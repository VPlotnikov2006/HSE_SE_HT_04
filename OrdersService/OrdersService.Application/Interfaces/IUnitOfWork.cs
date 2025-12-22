namespace OrdersService.Application.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
    Task RollbackAsync(CancellationToken ct);
}
