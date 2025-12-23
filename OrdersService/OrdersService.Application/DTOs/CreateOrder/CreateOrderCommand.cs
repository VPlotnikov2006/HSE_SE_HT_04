namespace OrdersService.Application.DTOs.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    List<CreateOrderItem> Items
);
