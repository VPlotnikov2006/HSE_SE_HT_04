using PaymentsService.Domain.Exceptions;

namespace PaymentsService.Domain.Accounts;

public class BankAccount
{
    private decimal _balance;
    public Guid UserId { get; private set; }
    public decimal Balance
    {
        get => _balance;
        set => _balance = (value >= 0) ? value : throw new InsufficientFundsException(UserId, _balance - value, _balance);
    }

    public BankAccount(Guid userId, decimal balance = 0)
    {
        UserId = userId;
        Balance = balance;
    }
}
