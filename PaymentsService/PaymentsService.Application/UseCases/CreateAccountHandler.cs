using PaymentsService.Application.DTOs;
using PaymentsService.Application.Interfaces.Repositories;
using PaymentsService.Domain.Accounts;
using PaymentsService.Domain.Exceptions;

namespace PaymentsService.Application.UseCases;

public class CreateAccountHandler(IAccountRepository accounts, IUnitOfWork uow)
{
    private readonly IAccountRepository _accounts = accounts;
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(CreateAccountCommand command, CancellationToken ct)
    {
        if (await _accounts.GetByIdAsync(command.UserId, ct) is not null)
        {
            throw new AccountAlreadyExistsException(command.UserId);
        }

        var account = new BankAccount(command.UserId, command.InitialBalance ?? 0m);
        await _accounts.AddAsync(account, ct);
        await _uow.CommitAsync(ct);
    }
}
