using OrdersService.Domain.Exceptions;

namespace OrdersService.Domain.Orders;

public class OrderItem(Guid orderId, Guid productId, uint quantity, decimal priceOnOrder)
{
    public Guid ItemId { get; private set; } = Guid.NewGuid();
    public Guid OrderId { get; private set; } = orderId;
    public Guid ProductId { get; private set; } = productId;
    public uint Quantity { get; private set; } = 
        quantity > 0 ? quantity : throw new OrderItemQuantityInvalidException(quantity);
    public decimal PriceOnOrder { get; private set; } = 
        priceOnOrder >= 0 ? priceOnOrder : throw new ProductInvalidPriceException(priceOnOrder);
}
