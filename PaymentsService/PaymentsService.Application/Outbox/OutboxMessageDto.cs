namespace PaymentsService.Application.Outbox;

public record class OutboxMessageDto(
    Guid OrderId,
    string NewStatus,
    DateTime CreatedAt
);
