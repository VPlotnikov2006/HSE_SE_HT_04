namespace OrdersService.Application.Outbox;

public class OutboxMessage
{
    public Guid OrderId { get; private set; }
    public decimal Debit { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public bool Published { get; private set; }

    protected OutboxMessage() { }

    public OutboxMessage(Guid orderId, Guid userId, decimal debit)
    {
        OrderId = orderId;
        UserId = userId;
        Debit = debit;
        CreatedAt = DateTime.UtcNow;
        Published = false;
    }

    public OutboxMessage(OutboxMessageDto dto)
        : this(dto.OrderId, dto.UserId, dto.TotalPrice)
    {
    }

    public void Publish()
    {
        Published = true;
        PublishedAt = DateTime.UtcNow;
    }
}
