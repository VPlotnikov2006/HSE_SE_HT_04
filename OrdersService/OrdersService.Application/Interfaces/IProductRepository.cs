using OrdersService.Domain.Products;

namespace OrdersService.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken ct);
    Task<List<Product>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Product product, CancellationToken ct);
}
