using OrdersService.Domain.Products;

namespace OrdersService.Domain.Orders;

public class Order(Guid userId)
{
    public Guid OrderId { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; } = userId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public decimal TotalPrice { get; private set; } = 0m;
    public OrderStatus Status { get; set; } = OrderStatus.New;

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public void Add(Product product, uint quantity)
    {
        var item = new OrderItem(OrderId, product.ProductId, quantity, product.Price);
        _items.Add(item);
        TotalPrice += item.PriceOnOrder;
    }
}
