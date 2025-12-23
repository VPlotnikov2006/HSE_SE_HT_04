using OrdersService.Application.DTOs;
using OrdersService.Application.DTOs.CreateOrder;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Domain.Exceptions;
using OrdersService.Domain.Orders;

namespace OrdersService.Application.UseCases.CreateOrder;

public class CreateOrderHandler(
    IOrderRepository orders, 
    IProductRepository products, 
    IOutboxRepository outbox, 
    IUnitOfWork uow
)
{
    private readonly IOrderRepository _orders = orders;
    private readonly IProductRepository _products = products;
    private readonly IOutboxRepository _outbox = outbox;
    private readonly IUnitOfWork _uow = uow;

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        var productIds = command.Items.Select(i => i.ProductId).ToList();

        var products = await _products.GetByIdsAsync(productIds, ct);

        var missingIds = productIds.Except(products.Select(p => p.ProductId)).ToList();
        if (missingIds.Count != 0)
            throw new ProductNotFoundException(missingIds.First());

        var order = new Order(command.UserId);
        foreach (var item in command.Items)
        {
            var product = products.Single(p => p.ProductId == item.ProductId);
            order.Add(product, item.Quantity);
        }

        await _orders.AddAsync(order, ct);
        await _outbox.AddAsync(
            new OutboxMessageDto(
                order.OrderId, 
                order.UserId, 
                order.TotalPrice
            ), 
            ct
        );
        await _uow.CommitAsync(ct);

        return new CreateOrderResult(order.OrderId);
    }
}
