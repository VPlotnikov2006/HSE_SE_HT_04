namespace OrdersService.Domain.Exceptions;

public class OrderItemQuantityInvalidException(uint quantity) : 
    DomainException($"Invalid order item quantity: {quantity}. Must be greater than zero.")
{
    public uint Quantity { get; } = quantity;
}
