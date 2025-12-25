using PaymentsService.Application.DTOs;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Domain.Exceptions;

namespace PaymentsService.Application.UseCases;

public class GetBalanceHandler(IAccountRepository accounts)
{
    private readonly IAccountRepository _accounts = accounts;

    public async Task<GetBalanceResult> Handle(Guid UserId, CancellationToken ct)
    {
        var account = await _accounts.GetByIdAsync(UserId, ct) ?? 
            throw new AccountNotFoundException(UserId);
        
        return new GetBalanceResult(account.UserId, account.Balance);
    }
}
