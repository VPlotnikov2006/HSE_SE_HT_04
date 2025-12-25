namespace PaymentsService.Application.DTOs;

public record class CreateAccountCommand(
    Guid UserId,
    decimal? InitialBalance = null
);
