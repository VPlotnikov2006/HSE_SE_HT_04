using PaymentsService.Application.Enums;

namespace PaymentsService.Application.Outbox;

public class OutboxMessage
{
    protected OutboxMessage() { }

    public OutboxMessage(Guid orderId, WithdrawResult result)
    {
        OrderId = orderId;
        CreatedAt = DateTime.UtcNow;
        NewStatus = result switch
        {
            WithdrawResult.Success => "Finished",
            WithdrawResult.Fail => "Cancelled",
            _ => throw new NotImplementedException()
        };
        Published = false;
        PublishedAt = null;
    }

    public Guid OrderId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string NewStatus { get; private set; }
    public bool Published { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    public void Publish()
    {
        Published = true;
        PublishedAt = DateTime.UtcNow;
    }

    public OutboxMessageDto AsDto()
    {
        return new( OrderId, NewStatus, CreatedAt );
    }
}
