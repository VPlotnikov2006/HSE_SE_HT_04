using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Products;

public class Product(Guid productId, string name, ProductCategory category, decimal price, float weight)
{
    public Guid ProductId { get; private set; } = productId;
    public string Name { get; private set; } = name;
    public ProductCategory Category { get; private set; } = category;
    public decimal Price { get; private set; } = 
        price >= 0 ? price : throw new ProductInvalidPriceException(price);
    
    public float Weight { get; private set; } = weight;

    public Product(string name, ProductCategory category, decimal price, float weight) : 
        this(Guid.NewGuid(), name, category, price, weight) {}
}
