using OrdersService.Application.DTOs;
using OrdersService.Application.Interfaces.Repositories;

namespace OrdersService.Application.UseCases.ListOrders;

public class ListOrdersHandler(IOrderRepository orders)
{
    private readonly IOrderRepository _orders = orders;

    public Task<List<OrderSummaryDto>> Handle(CancellationToken ct)
    {
        return _orders.GetOrderSummariesAsync(ct);
    }
}
