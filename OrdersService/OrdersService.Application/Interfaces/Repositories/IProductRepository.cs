using OrdersService.Domain.Products;

namespace OrdersService.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken ct);
    Task<List<Product>> GetByIdsAsync(List<Guid> productIds, CancellationToken ct);
    Task<List<Product>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Product product, CancellationToken ct);
}
