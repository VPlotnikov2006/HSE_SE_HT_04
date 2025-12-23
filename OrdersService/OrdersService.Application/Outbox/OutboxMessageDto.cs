namespace OrdersService.Application.Outbox;

public record class OutboxMessageDto(
    Guid OrderId,
    Guid UserId,
    decimal TotalPrice
);
