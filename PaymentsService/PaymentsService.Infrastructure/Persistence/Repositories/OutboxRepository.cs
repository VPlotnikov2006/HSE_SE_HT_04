using Microsoft.EntityFrameworkCore;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Application.Outbox;
using PaymentsService.Infrastructure.Persistence.DbContexts;

namespace PaymentsService.Infrastructure.Persistence.Repositories;

public class OutboxRepository(PaymentsDbContext db) : IOutboxRepository
{
    private readonly PaymentsDbContext _db = db;

    public async Task AddAsync(OutboxMessage message, CancellationToken ct)
        => await _db.Outbox.AddAsync(message, ct);

    public async Task<List<OutboxMessage>> GetUnpublishedAsync(uint batchSize, CancellationToken ct)
        => await _db.Outbox
            .Where(x => !x.Published)
            .OrderBy(x => x.CreatedAt)
            .Take((int)batchSize)
            .ToListAsync(ct);
}
