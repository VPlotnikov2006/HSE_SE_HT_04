namespace OrdersService.Domain.Exceptions;

public class ProductNotFoundException(Guid productId) : 
    DomainException($"Product with Id '{productId}' was not found.")
{
    public Guid ProductId { get; } = productId;
}
