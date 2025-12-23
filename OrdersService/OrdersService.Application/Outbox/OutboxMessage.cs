namespace OrdersService.Application.Outbox;

public class OutboxMessage(OutboxMessageDto message)
{
    public Guid OrderId { get; private set; } = message.OrderId;
    public decimal Debit { get; private set; } = message.TotalPrice;
    public Guid UserId { get; private set; } = message.UserId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; private set; } = null;
    public bool Published { get; private set; } = false;

    public void Publish()
    {
        Published = true;
        PublishedAt = DateTime.UtcNow;
    }
}
