using OrdersService.Domain.Orders;

namespace OrdersService.Application.UseCases.UpdateOrderStatus;

public record class UpdateOrderStatusQuery(
    Guid OrderId,
    OrderStatus NewStatus
);