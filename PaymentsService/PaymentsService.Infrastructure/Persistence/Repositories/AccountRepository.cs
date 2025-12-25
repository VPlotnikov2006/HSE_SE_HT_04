using Microsoft.EntityFrameworkCore;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Domain.Accounts;
using PaymentsService.Infrastructure.Persistence.DbContexts;

namespace PaymentsService.Infrastructure.Persistence.Repositories;

public class AccountRepository(PaymentsDbContext db) : IAccountRepository
{
    private readonly PaymentsDbContext _db = db;

    public async Task AddAsync(BankAccount account, CancellationToken ct)
        => await _db.Accounts.AddAsync(account, ct);

    public async Task<BankAccount?> GetByIdAsync(Guid userId, CancellationToken ct)
        => await _db.Accounts.FirstOrDefaultAsync(x => x.UserId == userId, ct);
}
