using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Infrastructure.Persistence.DbContexts;

namespace PaymentsService.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(PaymentsDbContext db) : IUnitOfWork
{
    private readonly PaymentsDbContext _db = db;

    public Task CommitAsync(CancellationToken ct)
        => _db.SaveChangesAsync(ct);

    public Task RollbackAsync(CancellationToken ct)
        => Task.CompletedTask;
}
