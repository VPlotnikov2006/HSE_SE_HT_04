namespace OrdersService.Application.DTOs;

public record class OutboxMessageDto(
    Guid OrderId,
    Guid UserId,
    decimal TotalPrice
);
