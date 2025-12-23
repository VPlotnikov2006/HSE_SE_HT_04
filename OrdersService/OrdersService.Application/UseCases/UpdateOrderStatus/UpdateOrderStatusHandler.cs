using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Domain.Exceptions;

namespace OrdersService.Application.UseCases.UpdateOrderStatus;

public class UpdateOrderStatusHandler(IOrderRepository orders, IUnitOfWork uow)
{
    private readonly IOrderRepository _orders = orders;
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateOrderStatusQuery query, CancellationToken ct)
    {
        var order = await _orders.GetByIdAsync(query.OrderId, ct) ?? throw new OrderNotFoundException(query.OrderId);
        order.Status = query.NewStatus;
        // TODO посмотреть работает ли без этой строчки
        // await _orders.UpdateAsync(order, ct);
        await _uow.CommitAsync(ct);
    }
}
