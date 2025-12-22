namespace OrdersService.Application.DTOs.CreateOrder;

public record CreateOrderCommandDto(
    Guid UserId,
    List<CreateOrderItemDto> Items
);
