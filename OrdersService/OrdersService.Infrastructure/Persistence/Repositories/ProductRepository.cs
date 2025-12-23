using Microsoft.EntityFrameworkCore;
using OrdersService.Application.Interfaces.Repositories;
using OrdersService.Domain.Products;
using OrdersService.Infrastructure.Persistence.DbContexts;

namespace OrdersService.Infrastructure.Persistence.Repositories;

public class ProductRepository(OrdersDbContext db) : IProductRepository
{
    private readonly OrdersDbContext _db = db;

    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken ct)
    {
        return await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId, ct);
    }

    public async Task<List<Product>> GetByIdsAsync(List<Guid> productIds, CancellationToken ct)
    {
        return await _db.Products
            .Where(p => productIds.Contains(p.ProductId))
            .ToListAsync(ct);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken ct)
    {
        return await _db.Products.ToListAsync(ct);
    }

    public async Task AddAsync(Product product, CancellationToken ct)
    {
        await _db.Products.AddAsync(product, ct);
    }
}
