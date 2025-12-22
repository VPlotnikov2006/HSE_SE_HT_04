using OrdersService.Domain.Orders;

namespace OrdersService.Application.DTOs.GetOrder;

public record class OrderDto(
    Guid OrderId,
    Guid UserId,
    OrderStatus Status,
    decimal TotalPrice,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);
