using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Infrastructure.Persistence.DbContexts;

namespace OrdersService.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(OrdersDbContext db) : IUnitOfWork
{
    private readonly OrdersDbContext _db = db;

    public Task CommitAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }

    public Task RollbackAsync(CancellationToken ct)
    {
        return Task.CompletedTask; // Т.к. транзакция начинается только в момент SaveChanges Rollback = не делать SaveChanges
    }
}
