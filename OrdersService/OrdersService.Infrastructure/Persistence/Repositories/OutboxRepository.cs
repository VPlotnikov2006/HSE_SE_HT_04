using Microsoft.EntityFrameworkCore;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Application.Outbox;
using OrdersService.Infrastructure.Persistence.DbContexts;

namespace OrdersService.Infrastructure.Persistence.Repositories;

public class OutboxRepository(OrdersDbContext db) : IOutboxRepository
{
    private readonly OrdersDbContext _db = db;

    public async Task AddAsync(OutboxMessageDto message, CancellationToken ct)
    {
        var entity = new OutboxMessage(message);
        await _db.OutboxMessages.AddAsync(entity, ct);
    }

    public async Task<List<OutboxMessage>> GetUnpublishedAsync(uint batchSize, CancellationToken ct)
    {
        return await _db.OutboxMessages
            .Where(m => !m.Published)
            .OrderBy(m => m.CreatedAt)
            .Take((int)batchSize)
            .ToListAsync(ct);
    }
}
