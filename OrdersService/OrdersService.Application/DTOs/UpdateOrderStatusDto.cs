namespace OrdersService.Application.DTOs;

public record UpdateOrderStatusDto(
    Guid OrderId,
    string NewStatus,
    DateTime CreatedAt
);
