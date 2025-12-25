using PaymentsService.Application.DTOs;
using PaymentsService.Application.Exceptions;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Domain.Exceptions;

namespace PaymentsService.Application.UseCases;

public class DepositHandler(IAccountRepository accounts, IUnitOfWork uow)
{
    private readonly IAccountRepository _accounts = accounts;
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateBalanceCommand command, CancellationToken ct)
    {
        var account = await _accounts.GetByIdAsync(command.UserId, ct) ?? 
            throw new AccountNotFoundException(command.UserId);

        if (command.BalanceDelta < 0)
        {
            throw new WrongDeltaSignException(command.BalanceDelta, '+');
        }

        account.Balance += command.BalanceDelta;

        await _uow.CommitAsync(ct);
    }
}
