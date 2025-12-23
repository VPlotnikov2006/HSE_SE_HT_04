using OrdersService.Application.DTOs;
using OrdersService.Application.DTOs.GetOrder;
using OrdersService.Domain.Orders;

namespace OrdersService.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken ct);
    Task<OrderDto?> GetAggregatedAsync(Guid orderId, CancellationToken ct);
    Task<List<Order>> GetAllAsync(CancellationToken ct);
    Task<List<OrderSummaryDto>> GetOrderSummariesAsync(CancellationToken ct);
    Task AddAsync(Order order, CancellationToken ct);
    Task UpdateAsync(Order order, CancellationToken ct);
}
