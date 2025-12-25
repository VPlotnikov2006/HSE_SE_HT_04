namespace PaymentsService.Domain.Exceptions;

public class InsufficientFundsException(Guid userId, decimal attemptedWithdrawal, decimal currentBalance) : 
    DomainException($"Cannot withdraw {attemptedWithdrawal} from user {userId}. Current balance: {currentBalance}.")
{
    public Guid UserId { get; } = userId;
    public decimal AttemptedWithdrawal { get; } = attemptedWithdrawal;
    public decimal CurrentBalance { get; } = currentBalance;
}
