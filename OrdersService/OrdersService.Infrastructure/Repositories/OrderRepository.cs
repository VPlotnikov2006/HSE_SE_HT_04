using Microsoft.EntityFrameworkCore;
using OrdersService.Application.DTOs;
using OrdersService.Application.DTOs.GetOrder;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Domain.Orders;
using OrdersService.Infrastructure.DbContexts;

namespace OrdersService.Infrastructure.Repositories;

public class OrderRepository(OrdersDbContext db) : IOrderRepository
{
    private readonly OrdersDbContext _db = db;

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken ct)
    {
        return await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.OrderId == orderId, ct);
    }

    public async Task<OrderDto?> GetAggregatedAsync(Guid orderId, CancellationToken ct)
    {
        var query = _db.Orders
            .Where(o => o.OrderId == orderId)
            .Select(o => new
            {
                Order = o,
                Items = _db.OrderItems
                    .Where(oi => oi.OrderId == o.OrderId)
                    .Join(
                        _db.Products,
                        oi => oi.ProductId,
                        p => p.ProductId,
                        (oi, p) => new OrderItemDto(
                            p.Name,
                            oi.PriceOnOrder,
                            oi.Quantity,
                            p.Weight,
                            p.Category
                        )
                    ).ToList()
            });

        var result = await query.FirstOrDefaultAsync(ct);

        if (result == null) return null;

        return new OrderDto(
            OrderId: result.Order.OrderId,
            UserId: result.Order.UserId,
            Status: result.Order.Status,
            TotalPrice: result.Order.TotalPrice,
            CreatedAt: result.Order.CreatedAt,
            Items: result.Items
        );
    }


    public async Task<List<Order>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Orders
            .Include(o => o.Items)
            .ToListAsync(ct);
    }

    public async Task<List<OrderSummaryDto>> GetOrderSummariesAsync(CancellationToken ct)
    {
        return await _db.Orders
            .Select(o => new OrderSummaryDto(
                o.OrderId,
                o.UserId,
                o.Status,
                o.TotalPrice,
                o.CreatedAt
            ))
            .ToListAsync(ct);
    }

    public async Task AddAsync(Order order, CancellationToken ct)
    {
        await _db.Orders.AddAsync(order, ct);
    }

    public async Task UpdateAsync(Order order, CancellationToken ct)
    {
        _db.Orders.Update(order);
        await Task.CompletedTask;
    }
}
