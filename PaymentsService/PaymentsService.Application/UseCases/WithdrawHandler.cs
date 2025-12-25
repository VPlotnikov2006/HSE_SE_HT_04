using PaymentsService.Application.DTOs;
using PaymentsService.Application.Enums;
using PaymentsService.Application.Exceptions;
using PaymentsService.Application.Interfaces;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Domain.Exceptions;

namespace PaymentsService.Application.UseCases;

public class WithdrawHandler(
    IAccountRepository accounts, 
    IInboxRepository inbox,
    IOutboxRepository outbox,
    IUnitOfWork uow
)
{
    private readonly IAccountRepository _accounts = accounts;
    private readonly IInboxRepository _inbox = inbox;
    private readonly IOutboxRepository _outbox = outbox;
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(Guid orderId, UpdateBalanceCommand command, CancellationToken ct)
    {
        if (command.BalanceDelta > 0)
        {
            throw new WrongDeltaSignException(command.BalanceDelta, '-');
        }

        if (! await _inbox.TryAddAsync(new(orderId), ct))
        {
            return;
        }

        var account = await _accounts.GetByIdAsync(command.UserId, ct) ?? 
            throw new AccountNotFoundException(command.UserId);
        
        try
        {
            account.Balance += command.BalanceDelta;

            await _outbox.AddAsync(new(orderId, WithdrawResult.Success), ct);
        }
        catch (InsufficientFundsException)
        {
            await _outbox.AddAsync(new(orderId, WithdrawResult.Fail), ct);
        }

        await _uow.CommitAsync(ct);
        
        return;
    }
}
