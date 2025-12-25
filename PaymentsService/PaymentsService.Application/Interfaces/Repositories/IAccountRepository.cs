using PaymentsService.Domain.Accounts;

namespace PaymentsService.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task AddAsync(BankAccount account, CancellationToken ct);
    Task<BankAccount?> GetByIdAsync(Guid UserId, CancellationToken ct);
}
