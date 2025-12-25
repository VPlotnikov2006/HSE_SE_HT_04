namespace PaymentsService.Domain.Exceptions;

public class AccountAlreadyExistsException(Guid userId) : DomainException($"Bank account for user {userId} already exists.")
{
    public Guid UserId { get; } = userId;
}
