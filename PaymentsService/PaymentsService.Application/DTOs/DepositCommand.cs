namespace PaymentsService.Application.DTOs;

public record class UpdateBalanceCommand(
    Guid UserId,
    decimal BalanceDelta
);
