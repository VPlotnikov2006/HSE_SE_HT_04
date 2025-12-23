namespace OrdersService.Application.UseCases.GetOrder;

public record class GetOrderQuery(
    Guid OrderId
);
