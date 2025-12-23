namespace OrdersService.Application.DTOs;

public record UpdateOrderStatusCommand(
    Guid OrderId,
    string NewStatus,
    DateTime CreatedAt
);
