namespace PaymentsService.Application.DTOs;

public record class GetBalanceResult(
    Guid UserId,
    decimal Balance
);
