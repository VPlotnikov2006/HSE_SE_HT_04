using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrdersService.Application.Interfaces.Messaging;
using OrdersService.Application.Outbox;

namespace OrdersService.Infrastructure.Messaging.Kafka;

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

    public Task PublishAsync(OutboxMessageDto message, CancellationToken ct)
    {
        var payload = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = message.OrderId.ToString(),
            Value = payload
        };

        return _producer.ProduceAsync(
            _options.OrdersOutboxTopic,
            kafkaMessage,
            ct
        );
    }

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }
}
