using PaymentsService.Application.Enums;

namespace PaymentsService.Application.Outbox;

public class OutboxMessage(Guid orderId, WithdrawResult result)
{
    public Guid OrderId { get; private set; } = orderId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string NewStatus { get; private set; } = result switch
    {
        WithdrawResult.Success => "Finished",
        WithdrawResult.Fail => "Cancelled",
        _ => throw new NotImplementedException()
    };

    public bool Published { get; private set; } = false;
    public DateTime? PublishedAt { get; private set; } = null;

    public void Publish()
    {
        Published = true;
        PublishedAt = DateTime.UtcNow;
    }

    public OutboxMessageDto AsDto()
    {
        return new (OrderId, NewStatus, CreatedAt);
    }
}
