namespace OrdersService.Domain.Exceptions;

public class OrderNotFoundException(Guid orderId) : 
    DomainException($"Order with Id '{orderId}' was not found.")
{
    public Guid OrderId { get; } = orderId;
}
