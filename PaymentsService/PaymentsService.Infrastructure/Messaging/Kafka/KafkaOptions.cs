namespace PaymentsService.Infrastructure.Messaging.Kafka;

public class KafkaOptions
{
    public string BootstrapServers { get; init; } = null!;
    public string OrdersOutboxTopic { get; init; } = null!;
    public string PaymentStatusTopic { get; init; } = null!;
    public string ConsumerGroupId { get; init; } = null!;
}
