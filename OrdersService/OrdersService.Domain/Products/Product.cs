using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Products;

public class Product(Guid productId, string name, ProductCategory category, decimal price)
{
    public Guid ProductId { get; private set; } = productId;
    public string Name { get; private set; } = name;
    public ProductCategory Category { get; private set; } = category;
    public decimal Price { get; private set; } = 
        price >= 0 ? price : throw new ProductInvalidPriceException(price);

    public Product(string name, ProductCategory category, decimal price) : this(Guid.NewGuid(), name, category, price) {}
}
