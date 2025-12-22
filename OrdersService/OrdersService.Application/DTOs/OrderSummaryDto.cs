using OrdersService.Domain.Orders;

namespace OrdersService.Application.DTOs;

public record class OrderSummaryDto(
    Guid OrderId,
    Guid UserId,
    OrderStatus Status,
    decimal TotalPrice,
    DateTime CreatedAt
);