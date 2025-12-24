using OrdersService.Domain.Products;

namespace OrdersService.Infrastructure.Persistence.DbContexts.Seed;

internal static class ProductSeed
{
    public static IReadOnlyCollection<Product> Data =>
    [
        new Product(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "Christmas Sweater",
            ProductCategory.ApparelAndAccessories,
            2999.00m,
            0.6f
        ),
        new Product(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Wireless Headphones",
            ProductCategory.Electronics,
            8999.00m,
            0.4f
        ),
        new Product(
            Guid.Parse("33333333-3333-3333-3333-333333333333"),
            "Premium Coffee Beans",
            ProductCategory.FoodAndBeverage,
            1499.00m,
            1.0f
        ),
        new Product(
            Guid.Parse("44444444-4444-4444-4444-444444444444"),
            "Vitamin C Serum",
            ProductCategory.HealthAndBeauty,
            2499.00m,
            0.15f
        ),
        new Product(
            Guid.Parse("55555555-5555-5555-5555-555555555555"),
            "Smart Fitness Watch",
            ProductCategory.Electronics,
            12999.00m,
            0.3f
        )
    ];
}
