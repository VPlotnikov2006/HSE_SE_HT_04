namespace PaymentsService.Application.Inbox;

public class InboxMessage(Guid orderId)
{
    public Guid OrderId { get; private set; } = orderId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
