using OrdersService.Application.DTOs.GetOrder;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Application.UseCases.GetOrder;

public sealed class GetOrderHandler(IOrderRepository orders)
{
    private readonly IOrderRepository _orders = orders;

    public async Task<OrderDto> Handle(GetOrderQuery query, CancellationToken ct)
    {
        var order = await _orders.GetAggregatedAsync(query.OrderId, ct) ?? throw new OrderNotFoundException(query.OrderId);
        return order;
    }
}
