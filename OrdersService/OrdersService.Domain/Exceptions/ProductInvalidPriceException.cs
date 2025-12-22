namespace OrdersService.Domain.Exceptions;

public class ProductInvalidPriceException(decimal price) : 
    DomainException($"Invalid product price: {price}. Price must be non-negative.")
{
    public decimal Price { get; } = price;
}
