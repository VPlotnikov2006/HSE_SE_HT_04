namespace OrdersService.Application.DTOs.CreateOrder;

public record CreateOrderItem(
    Guid ProductId,
    uint Quantity
);
