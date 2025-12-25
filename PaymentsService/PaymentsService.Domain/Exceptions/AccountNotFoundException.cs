namespace PaymentsService.Domain.Exceptions;

public class AccountNotFoundException(Guid userId) : DomainException($"Bank account for user {userId} was not found.")
{
    public Guid UserId { get; } = userId;
}
