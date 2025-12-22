namespace OrdersService.Application.DTOs.CreateOrder;

public record CreateOrderItemDto(
    Guid ProductId,
    uint Quantity
);
