using Microsoft.EntityFrameworkCore;
using PaymentsService.Application.Inbox;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Infrastructure.Persistence.DbContexts;

namespace PaymentsService.Infrastructure.Persistence.Repositories;

public class InboxRepository(PaymentsDbContext db) : IInboxRepository
{
    private readonly PaymentsDbContext _db = db;

    public async Task<bool> TryAddAsync(InboxMessage message, CancellationToken ct)
    {
        if (await _db.Inbox.AnyAsync(x => x.OrderId == message.OrderId, ct))
            return false;

        await _db.Inbox.AddAsync(message, ct);
        return true;
    }
}
