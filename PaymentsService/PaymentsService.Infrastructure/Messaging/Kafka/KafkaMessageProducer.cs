using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PaymentsService.Application.Interfaces.Messaging;
using PaymentsService.Application.Outbox;

namespace PaymentsService.Infrastructure.Messaging.Kafka;

public sealed class KafkaMessageProducer : IMessageProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly KafkaOptions _options;

    public KafkaMessageProducer(IOptions<KafkaOptions> options)
    {
        _options = options.Value;

        var config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            Acks = Acks.All
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync(OutboxMessageDto message, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(message);

        var kafkaMsg = new Message<string, string>
        {
            Key = message.OrderId.ToString(),
            Value = json
        };

        await _producer.ProduceAsync(_options.PaymentStatusTopic, kafkaMsg, ct);
    }

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}
